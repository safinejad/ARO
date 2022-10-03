namespace API.Dtos;

public class HotelAvailableDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public int Stars { get; set; }
    public string AddressBaseLine { get; set; }
    public string DistrictName { get; set; }
    public string DistrictId { get; set; }
    public IEnumerable<FreeFacilityDto> HighlightedFacilities { get; set; }
    public bool SustainableBadge { get; set; }
    public bool Promoted { get; set; }
    public decimal? OverallScore { get; set; }
    public int NumberOfReviews { get; set; }
    public decimal? ComfortRate { get; set; }

}