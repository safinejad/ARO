using Contracts.Annotations;

namespace Contracts.DataModel;

public class Sleep
{
    public long Id { get; set; }
    public long RoomId { get; set; }
    public HotelRoomPrice Room { get; set; }
    public SleepTypeEnum Type { get; set; }
    public int Count { get; set; }
    public int ExtraCount { get; set; }
    [Money]
    public decimal? ExtraPrice { get; set; }
}