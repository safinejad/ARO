using Contracts.DataModel;

namespace API.Dtos;

public class FacilityDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public FacilityTypeEnum FacilityType { get; set; }
}