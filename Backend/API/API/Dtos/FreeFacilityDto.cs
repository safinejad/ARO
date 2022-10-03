using Contracts.Annotations;
using Contracts.DataModel;

namespace API.Dtos;

public class FreeFacilityDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public FacilityTypeEnum FacilityType { get; set; }
}
public class RoomFacilityDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public FacilityTypeEnum FacilityType { get; set; }
    [Money]
    public decimal? ExtraCharge { get; set; }
    [Money]
    public decimal? ExtraChargePerPerson { get; set; }
}
public class HotelFacilityDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public FacilityTypeEnum FacilityType { get; set; }
    public bool ExtraChargeRequired { get; set; }

}