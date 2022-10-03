namespace Contracts.Requests;

public class AvailableRequest
{
    public long GeographicBoundary { get; set; }
    public int RoomCount { get; set; } = 1;
    public DateTime CheckOut { get; set; }
    public DateTime CheckIn { get; set; }
    public int AdultCount { get; set; } = 1;
    public int[] ChildAges { get; set; } = null;
}
public class RoomRequest
{
    public long HotelId { get; set; }
    public int RoomCount { get; set; } = 1;
    public DateTime CheckOut { get; set; }
    public DateTime CheckIn { get; set; }
    public int AdultCount { get; set; } = 1;
    public int[] ChildAges { get; set; } = null;
}