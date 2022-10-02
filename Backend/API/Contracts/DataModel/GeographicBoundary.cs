namespace Contracts.DataModel;

public class GeographicBoundary
{
    public long Id { get; set; }
    public GeographicBoundaryTypeEnum BoundaryType { get; set; }
    public long? ParentId { get; set; }
    public GeographicBoundary? Parent { get; set; }
    public GeographicBoundary Child { get; set; }
    public string Name { get; set; }
    public string MapLink { get; set; }
    public decimal CenterLatitude { get; set; }
    public decimal CenterLongitude { get; set; }
    public bool IsTopDestination { get; set; }
    public ICollection<Hotel> Hotels { get; set; }
    public ICollection<Neighbourhood> Neighbourhoods { get; set; }
}

public class Neighbourhood
{
    public long Id { get; set; }
    public string Name { get; set; }
    public long GeographicBoundaryId { get; set; }
    public GeographicBoundary GeographicBoundary { get; set; } //Nisantasi (3KM from center according to Lat/Lon)
    public decimal CenterLatitude { get; set; }
    public decimal CenterLongitude { get; set; }
    public PropertyTypeEnum PropertyType { get; set; }
}

public enum PropertyTypeEnum
{
    Airport,
    Museum,
    Restaurant,
    Nature,
    Beach,
    ShoppingCenter,
    TopAttraction,
    PublicTransport,

    
}