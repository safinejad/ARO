namespace Contracts.DataModel;

public class Hotel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public int Stars { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public string Description { get; set; }//Lazy
    public string AddressBaseLine { get; set; }
    public long GeographicBoundaryId { get; set; }
    public GeographicBoundary GeographicBoundary { get; set; } //Nisantasi (3KM from center according to Lat/Lon)
    public int PaymentCurrencyId { get; set; }
    public Currency PaymentCurrency { get; set; }
    public ICollection<HotelFacility> Facilities { get; set; } //Dog-Friendly //Ski-To-Door //Metro Access //Excellent Location
    public ICollection<HotelRoomPriceAvailable> Rooms { get; set; }
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