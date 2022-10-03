namespace Contracts.Requests;

public class HotelAvailableRequest
{
    public long? HotelId { get; set; } = null;
    public long? GeographicBoundary { get; set; } = null;
    public int RoomCount { get; set; } = 1;
    public DateTime CheckOut { get; set; }
    public DateTime CheckIn { get; set; }
    public int AdultCount { get; set; } = 1;
    public int[] ChildAges { get; set; } = null;
}