using Contracts.Annotations;

namespace Contracts.DataModel;

public class DiscountOnCount
{
    public long Id { get; set; }
    public long RoomId { get; set; }
    public HotelRoomPriceAvailable Room { get; set; }
    [Money]
    public decimal DiscountPercent { get; set; }
    public int Count { get; set; }
}