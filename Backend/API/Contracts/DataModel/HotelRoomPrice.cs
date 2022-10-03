using Contracts.Annotations;

namespace Contracts.DataModel;

public class HotelRoomPrice
{
    public Hotel Hotel { get; set; }

    public long Id { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public long HotelId { get; set; }
    [Money]
    public decimal? PreviousPrice { get; set; }
    [Money] 
    public decimal Price { get; set; }
    [Money]
    public decimal? PriceTax { get; set; }
    public ICollection<DiscountOnCount> Discounts { get; set; }
    public string RoomName { get; set; }
    public int LeftCount { get; set; }
    public int TotalCount { get; set; }
    public int? SqMeter { get; set; }
    [Money]
    public decimal PayBackCredit { get; set; }
    public ICollection<Sleep> Sleeps { get; set; }
    public ICollection<RoomFacility> Facilities { get; set; } //Iron //BedAndBreakfast //AllInclusive //FullBoard //Lazy
    public ICollection<CancellationPolicy> CancellationPolicies { get; set; }
}