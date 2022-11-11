using Contracts.Annotations;
using Contracts.DataModel;

namespace API.Dtos;

public class HotelDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public int Stars { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public string Description { get; set; }//Lazy
    public DistrictDto District { get; set; }
    public CurrencyDto PaymentCurrency { get; set; }
    public IEnumerable<HotelFacilityDto> Facilities { get; set; }
    public bool SustainableBadge { get; set; }
    public bool Promoted { get; set; }
    public decimal? OverallScore { get; set; }
    public int NumberOfReviews { get; set; }
    public decimal? ComfortRate { get; set; }
    public decimal? StaffRate { get; set; }
    public decimal? FacilityRate { get; set; }
    public decimal? CleanlinessRate { get; set; }
    public decimal? LocationRate { get; set; }
    public decimal? ValueRate { get; set; }
    public short MaxRoomsInReservation { get; set; }
}

public class DistrictDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string MapLink { get; set; }
    public decimal CenterLatitude { get; set; }
    public decimal CenterLongitude { get; set; }
    public decimal DistanceFromHotel { get; set; }

}

public class CurrencyDto
{
    public string Name { get; set; }
    public string Sign { get; set; }
}

public class HotelWithNeighbourhoodDto
{
    public HotelDto Hotel { get; set; }
    public IEnumerable<NeighbourhoodDto> Neighbourhoods { get; set; }
}

public class NeighbourhoodDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public decimal CenterLatitude { get; set; }
    public decimal CenterLongitude { get; set; }
    public PropertyTypeEnum PropertyType { get; set; }
    public decimal DistanceFromHotel { get; set; }
}

public class RoomDto
{
    [Money]
    public decimal? PreviousPrice { get; set; }
    [Money]
    public decimal Price { get; set; }
    [Money]
    public decimal? PriceTax { get; set; }
    public IEnumerable<DiscountOnCountDto> Discounts { get; set; }
    public string RoomName { get; set; }
    public int LeftCount { get; set; }
    public int TotalCount { get; set; }
    public int? SqMeter { get; set; }
    [Money]
    public decimal PayBackCredit { get; set; }
    public IEnumerable<SleepDto> Sleeps { get; set; }
    public IEnumerable<RoomFacilityDto> Facilities { get; set; } //Iron //BedAndBreakfast //AllInclusive //FullBoard //Lazy
    public IEnumerable<CancellationPolicyDto> CancellationPolicies { get; set; }
}

public class DiscountOnCountDto
{
    public decimal DiscountPercent { get; set; }
    public int Count { get; set; }
}

public class CancellationPolicyDto
{
    public int? DayBeforeCheckOut { get; set; }
    public TimeSpan? TimeBeforeCheckOut { get; set; }
    [Money]
    public decimal? CashCharge { get; set; }
    public decimal? PercentCharge { get; set; }

}