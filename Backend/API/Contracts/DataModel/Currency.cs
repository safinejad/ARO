namespace Contracts.DataModel;

public class Currency
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Sign { get; set; }
    public decimal ExchangeRateFromUSDollar { get; set; }
    public ICollection<Hotel> Hotels { get; set; }
}