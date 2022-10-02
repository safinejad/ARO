namespace Contracts.DataModel;

public class HotelFacility
{
    public long Id { get; set; }
    public long HotelId { get; set; }
    public int FacilityId { get; set; }
    public Facility Facility { get; set; }
    public Hotel Hotel { get; set; }//Lazy
    public int? SearchMatchAdult { get; set; } //{Adult:2,Child:1}
    public int? SearchMatchChild { get; set; } //{Adult:2,Child:1}
    public bool ExtraChargeRequired { get; set; }
}