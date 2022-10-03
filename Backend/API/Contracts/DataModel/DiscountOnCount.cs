using Contracts.Annotations;

namespace Contracts.DataModel;

public class DiscountOnCount
{
    public long Id { get; set; }
    public long RoomId { get; set; }
    public HotelRoomPrice Room { get; set; }
    public decimal DiscountPercent { get; set; }
    public int Count { get; set; }
}