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
        public AvailableController(IAvailableBusinessService availableBusinessService, IMapper mapper)
        {
            _availableService = availableBusinessService;
            _mapper = mapper;
        }

        private readonly IAvailableBusinessService _availableService;
        private readonly IMapper _mapper;


        [HttpGet("{GeographicBoundary?}/{HotelId?}/{AdultCount}/{ChildAges?}/{CheckIn}/{CheckOut}/{RoomCount?}")]
        public ActionResult<IEnumerable<HotelAvailableDto>> Available( [FromRoute]HotelAvailableRequest request)
        {
            var availables = _availableService.GetAvailable(request);
            if (!availables.Any()) return NotFound();
            var converted = _mapper.Map<IEnumerable<HotelAvailableDto>>(availables);
            return Ok(converted);
        }
    }
}