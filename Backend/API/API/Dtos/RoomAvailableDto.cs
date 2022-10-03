using Contracts.Annotations;

namespace API.Dtos;

public class RoomAvailableDto
{
    [Money]
    public decimal Price { get; set; }
    [Money]
    public decimal? PriceTax { get; set; }
    public IEnumerable<SleepDto> Sleeps { get; set; }
    public bool HasFreeCancellation { get; set; }
    public FreeFacilityDto DiningFacility { get; set; }
    public decimal PayBackCredit { get; set; }
    public int LeftCount { get; set; }
}