using System.Text.Json;
using API.Dtos;
using AutoMapper;
using Contracts.Requests;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AvailableController : ControllerBase
    {
        public static decimal CalculateDistanceMeters(decimal lat1, decimal lon1, decimal lat2, decimal lon2)
        {
            return (decimal) (Math.Acos(Math.Sin((double)lat1) * Math.Sin((double)lat2) +
                             Math.Cos((double)lat1) * Math.Cos((double)lat2) * Math.Cos((double)lon2 - (double)lon1)) * (double)63.71);
        }
        public AvailableController(IAvailableBusinessService availableBusinessService, IMapper mapper, IHttpContextAccessor accessor)
        {
            _availableService = availableBusinessService;
            _accessor = accessor;
            _mapper = mapper;
        }

        private readonly IAvailableBusinessService _availableService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;

        [HttpGet("Currencies")]
        public async Task<ActionResult<IEnumerable<CurrencyDto>>> GetSupportedCurrencies()
        {
            var currencies = await _availableService.GetCurrencies();
            var converted = _mapper.Map<IEnumerable<CurrencyDto>>(currencies);
            return Ok(converted);
        }

        [HttpGet("{geographicBoundary}/{checkIn}/{checkOut}")] //TODO: better for seo : Change geoId => geoCode
        public async Task<ActionResult<IEnumerable<AvailableDto>>> Available([FromRoute] long geographicBoundary,
            [FromRoute] DateTime checkIn, [FromRoute] DateTime checkOut,
            [FromQuery] int adultCount = 3, [FromRoute] int roomCount = 1,
            [FromQuery] int[] childAges = null)
        {
            var request = new AvailableRequest()
            {
                AdultCount = adultCount,
                ChildAges = childAges,
                CheckIn = checkIn,
                CheckOut = checkOut,
                GeographicBoundary = geographicBoundary,
                RoomCount = roomCount
            };
            var availables = await _availableService.GetAvailable(request);
            if (!availables.Any()) return NotFound();
            var converted = _mapper.Map<IEnumerable<AvailableDto>>(availables);
            return Ok(converted);
        }


        [HttpGet("{hotelId}")] //TODO: better for seo : Change geoId => geoCode
        public async Task<ActionResult<HotelWithNeighbourhoodDto>> Hotel([FromRoute] long hotelId)
        {
            var hotelWithNeighbourhood = await _availableService.GetHotel(hotelId);
            if (hotelWithNeighbourhood == null || hotelWithNeighbourhood.Hotel == null) return NotFound();
            var converted = _mapper.Map<HotelWithNeighbourhoodDto>(hotelWithNeighbourhood);
            if (converted != null)
            {
                if (converted.Hotel.District != null)
                {
                    converted.Hotel.District.DistanceFromHotel = CalculateDistanceMeters(converted.Hotel.Latitude,
                        converted.Hotel.Longitude, converted.Hotel.District.CenterLatitude,
                        converted.Hotel.District.CenterLongitude);
                }

                if (converted.Neighbourhoods != null && converted.Neighbourhoods.Any())
                {

                    foreach (var neighbourhoodDto in converted.Neighbourhoods)
                    {
                        neighbourhoodDto.DistanceFromHotel = CalculateDistanceMeters(converted.Hotel.Latitude,
                            converted.Hotel.Longitude, neighbourhoodDto.CenterLatitude,
                            neighbourhoodDto.CenterLongitude);
                    }
                }
            }

            return Ok(converted);
        }


        [HttpGet("{hotelId}/Rooms/{checkIn}/{checkOut}")] //TODO: better for seo : Change geoId => geoCode
        public async Task<ActionResult<IEnumerable<RoomDto>>> HotelRooms([FromRoute] long hotelId, [FromRoute] DateTime checkIn, [FromRoute] DateTime checkOut,
            [FromQuery] int adultCount = 3, [FromRoute] int roomCount = 1,
            [FromQuery] int[] childAges = null)
        {
            var request = new RoomRequest()
            {
                AdultCount = adultCount,
                ChildAges = childAges,
                CheckIn = checkIn,
                CheckOut = checkOut,
                HotelId = hotelId,
                RoomCount = roomCount
            };
            var rooms = await _availableService.GetHotelRooms(request);
            if (!rooms.Any()) return NotFound();
            var converted = _mapper.Map<IEnumerable<RoomDto>>(rooms);
            return Ok(converted);
        }
    }
}