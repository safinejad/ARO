namespace API.Dtos;

public class RoomAvailableDto
{
    public decimal Price { get; set; }
    public decimal? PriceTax { get; set; }
    public IEnumerable<SleepDto> Sleeps { get; set; }
    public bool HasFreeCancellation { get; set; }
    public FacilityDto DiningFacility { get; set; }
    public decimal PayBackCredit { get; set; }
    public int LeftCount { get; set; }
}