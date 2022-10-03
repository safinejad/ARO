namespace API.Dtos;

public class HotelAvailableDto
{
    public HotelDto Hotel { get; set; }
    public RoomAvailableDto FirstRoomAvailable { get; set; }
}