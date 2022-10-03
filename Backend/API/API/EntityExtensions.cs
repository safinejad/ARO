using Contracts.DataModel;

namespace API;

public static class EntityExtensions
{
    public static GeographicBoundary GetBoundary(this GeographicBoundary boundary, GeographicBoundaryTypeEnum typeEnum)
    {
        if (boundary == null) return null;
        if (boundary.BoundaryType == typeEnum) return boundary;
        return boundary.Parent.GetBoundary(typeEnum);
    }
}