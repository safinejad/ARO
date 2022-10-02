namespace Contracts.DataModel;

public class Facility//ManyToManyRoomFacility
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public bool Highlighted { get; set; }
    public FacilityTypeEnum FacilityType { get; set; }
    public ICollection<RoomFacility> RoomFacilities { get; set; }//Lazy
    public ICollection<HotelFacility> HotelFacilities { get; set; } //Lazy
}