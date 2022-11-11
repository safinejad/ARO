using System.ComponentModel;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Contracts.Annotations;
using Contracts.DataModel;
using Service;

namespace API;

public class MoneyConverter : JsonConverter<object>
{
    private readonly IHttpContextAccessor _http;
    private Dictionary<string, Currency> _currencies;

    public MoneyConverter(IHttpContextAccessor httpContextAccessor)
    {
        _http = httpContextAccessor;
        _currencies = null;
    }
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsClass &&
               typeToConvert.GetProperties().Any(x => x.GetCustomAttribute<MoneyAttribute>() != null);
    }

    public override object Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        return JsonDocument.ParseValue(ref reader).RootElement.Clone() as object;
    }

    public override void Write(
        Utf8JsonWriter writer,
        object objectToWrite,
        JsonSerializerOptions options)
    {
        if (_http != null && _http.HttpContext != null && objectToWrite != null )
        {
            var _availableService = _http.HttpContext.RequestServices.GetService<IAvailableBusinessService>();

            if (_currencies == null || !_currencies.Any())
            {
                _currencies = _availableService.GetCurrencies().GetAwaiter().GetResult()
                    .ToDictionary(x => x.Name, x => x, StringComparer.InvariantCultureIgnoreCase);
            }
            var typeToConvert = objectToWrite.GetType();
            if (typeToConvert.IsClass)
            {
                if (!_http.HttpContext.Request.Cookies.TryGetValue("Currency", out var cur) || string.IsNullOrWhiteSpace(cur))
                {
                    if (_http.HttpContext.Request.Headers.TryGetValue("Currency", out var values) &&
                        values.Count > 0)
                    {
                        cur = values.First();
                    }
                }
                
                if (!string.IsNullOrWhiteSpace(cur) && _currencies.TryGetValue(cur, out var currency) &&
                    currency != null && currency.ExchangeRateFromUSDollar != 0 &&
                    currency.ExchangeRateFromUSDollar != 1)
                {
                    var propsToConvert = typeToConvert.GetProperties()
                        .Where(x => x.GetCustomAttribute<MoneyAttribute>() != null); //TODO: To be cached.

                    foreach (var propertyInfo in propsToConvert)
                    {
                        if (typeof(decimal).IsAssignableFrom(propertyInfo.PropertyType) ||
                             typeof(decimal?).IsAssignableFrom(propertyInfo.PropertyType))
                        {
                            var value = propertyInfo.GetValue(objectToWrite);
                            if (value != null)
                            {
                                var decValue = (decimal)value;
                                var newValue = Math.Ceiling(decValue / currency.ExchangeRateFromUSDollar);
                                propertyInfo.SetValue(objectToWrite, newValue);
                            }
                        }
                    }
                }
            }
        }

        var newOpt = new JsonSerializerOptions(options);
        var thisConverter = newOpt.Converters.FirstOrDefault(x => x.GetType().Name == this.GetType().Name);
        if (thisConverter != null) newOpt.Converters.Remove(thisConverter);
        JsonSerializer.Serialize(writer, objectToWrite, newOpt);
    }


}