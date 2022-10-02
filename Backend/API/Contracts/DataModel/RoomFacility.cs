using Contracts.Annotations;

namespace Contracts.DataModel;

public class RoomFacility//OneToManyRoom
{
    public long Id { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public int FacilityId { get; set; }
    public Facility Facility { get; set; }
    public long RoomId { get; set; }//Lazy
    public HotelRoomPriceAvailable Room { get; set; }//Lazy
    [Money]
    public decimal? ExtraCharge { get; set; }
    [Money]
    public decimal? ExtraChargePerPerson { get; set; }
}