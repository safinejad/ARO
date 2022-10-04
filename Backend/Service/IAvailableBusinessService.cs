using Contracts.DataModel;
using Contracts.ObjectModel;
using Contracts.Requests;

namespace Service;

public interface IAvailableBusinessService
{
    //This is a testing method
    Task<IEnumerable<HotelAvailable>> GetAvailable(AvailableRequest request);
    Task<HotelWithNeighbourhood> GetHotel(long hotelId);
    Task<IEnumerable<HotelRoomPrice>> GetHotelRooms(RoomRequest request);
    Task<IEnumerable<Currency>> GetCurrencies();
}