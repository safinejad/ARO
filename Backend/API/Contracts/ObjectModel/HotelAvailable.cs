using System.Security.Cryptography;
using Contracts.DataModel;

namespace Contracts.ObjectModel
{
    public class HotelAvailable
    {
        public Hotel Hotel { get; set; }
        public HotelRoomPriceAvailable FirstAvailableRoom { get; set; }
    }
}