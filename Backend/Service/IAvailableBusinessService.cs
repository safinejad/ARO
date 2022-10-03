using Contracts.ObjectModel;
using Contracts.Requests;

namespace Service;

public interface IAvailableBusinessService
{
    //This is a testing method
    IEnumerable<HotelAvailable> GetAvailable(HotelAvailableRequest request);
}