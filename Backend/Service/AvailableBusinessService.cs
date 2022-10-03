using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.DataModel;
using Contracts.ObjectModel;
using Contracts.Requests;
using Data;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    public class AvailableBusinessService : IAvailableBusinessService
    {
        private readonly BookingContext _dbContext;
        private readonly Dictionary<long, GeographicBoundary> _geosCached;
        private readonly Dictionary<long, List<GeographicBoundary>> _geosCachedByParent;

        public AvailableBusinessService(BookingContext dbContext)
        {
            _dbContext = dbContext;
            _geosCached = dbContext.GeographicBoundaries.ToList().ToDictionary(x => x.Id, x => x);
            _geosCachedByParent = dbContext.GeographicBoundaries.ToList().GroupBy(x => x.ParentId ?? 0L)
                .ToDictionary(x => x.Key, x => x.ToList());
        }
        public IEnumerable<HotelAvailable> GetAvailable(HotelAvailableRequest request) //This is a testing method
        {
            var hotelRoomPrices = _dbContext.HotelRoomPrices
                .Where(x => x.LeftCount >= request.RoomCount);
            if (request.HotelId.HasValue && request.HotelId.Value > 0)
            {
                hotelRoomPrices = hotelRoomPrices.Where(x => x.HotelId == request.HotelId.Value);
            }

            if (request.GeographicBoundary.HasValue && request.GeographicBoundary.Value > 0)
            {
                _geosCached.TryGetValue(request.GeographicBoundary.Value, out var enteredGeo);
                List<GeographicBoundary> geos = new List<GeographicBoundary>() { enteredGeo };
                for (int i = 0; i < geos.Count; i++)
                {
                    var geo = geos[i];
                    if (!geo.ParentId.HasValue) continue;
                    if (_geosCachedByParent.TryGetValue(geo.ParentId.Value, out var subGeos) && subGeos != null &&
                        geos.Count > 0)
                    {
                        geos.AddRange(subGeos);
                    }
                }

                var geosIds = geos.Select(x => x.Id).Distinct();

                hotelRoomPrices = hotelRoomPrices.Where(x => geosIds.Contains(x.Hotel.GeographicBoundaryId));
            }
            //No Strict Check for Child Age, because this is a Test!
            var totalCount = request.AdultCount + request.ChildAges.Count(x => x >= 2);
            hotelRoomPrices = hotelRoomPrices.Where(x => totalCount <=
                                                         x.Sleeps.Count(s => s.Type == SleepTypeEnum.SofaBed ||
                                                                             s.Type == SleepTypeEnum.SingleBed) +
                                                         x.Sleeps.Count(s => s.Type != SleepTypeEnum.SofaBed &&
                                                                             s.Type != SleepTypeEnum.SingleBed) * 2);

            hotelRoomPrices = hotelRoomPrices
                .Include(x => x.Sleeps)
                .Include(x => x.Facilities).ThenInclude(x => x.Facility);
                //.Include(x => x.Hotel).ThenInclude(x => x.Facilities);

            //No Check for CheckIn/CheckOutDate because this is a Test!
            return hotelRoomPrices.GroupBy(x => x.HotelId).Select(x =>
                new HotelAvailable()
                {
                    Hotel = x.First().Hotel,
                    FirstAvailableRoom = x.OrderBy(r => r.Price).FirstOrDefault()
                });
        }
    }


}
