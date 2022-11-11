using System.ComponentModel;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Text.Json.Serialization;
using API.Dtos;
using Contracts.Annotations;
using Contracts.DataModel;
using Service;

namespace API;

public class MoneyConverter1 : JsonConverter<RoomDto>
{
    private readonly IHttpContextAccessor _http;
    private Dictionary<string, Currency> _currencies;
    private readonly MoneyConverter _converter;

    public MoneyConverter1(IHttpContextAccessor httpContextAccessor)
    {
        _http = httpContextAccessor;
        _converter = new MoneyConverter(httpContextAccessor);
        _currencies = null;
    }
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsClass &&
               typeof(RoomDto).IsAssignableFrom(typeToConvert);
    }

    public override RoomDto Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        return _converter.Read(ref reader, typeToConvert, options) as RoomDto;
    }

    public new void Write(
        Utf8JsonWriter writer,
        object objectToWrite,
        JsonSerializerOptions options)
    {
        this.Write(writer, (RoomDto)objectToWrite, options);
    }

    public override void Write(
        Utf8JsonWriter writer,
        RoomDto objectToWrite,
        JsonSerializerOptions options)
    {
        var newOpt = new JsonSerializerOptions(options);
        var thisConverter = newOpt.Converters.FirstOrDefault(x => x.GetType().Name == this.GetType().Name);
        if (thisConverter != null) newOpt.Converters.Remove(thisConverter);
        JsonSerializer.Serialize(writer, objectToWrite, newOpt);

        _converter.Write(writer, objectToWrite, newOpt);
    }


}