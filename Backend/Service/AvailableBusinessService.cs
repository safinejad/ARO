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

        public async Task<IEnumerable<HotelAvailable>> GetAvailable(AvailableRequest request) //This is a testing method
        {
            var hotelRoomPrices = _dbContext.HotelRoomPrices
                .Where(x => x.LeftCount >= request.RoomCount);
            //if (request.HotelId.HasValue && request.HotelId.Value > 0)
            //{
            //    hotelRoomPrices = hotelRoomPrices.Where(x => x.HotelId == request.HotelId.Value);
            //}

            if (request.GeographicBoundary > 0)
            {
                _geosCached.TryGetValue(request.GeographicBoundary, out var enteredGeo);
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
            var childCount = request.ChildAges.Count(x => x >= 2);
            var totalCount = request.AdultCount + childCount;
            hotelRoomPrices = hotelRoomPrices.Where(x => totalCount ==
                                                         x.Sleeps.Count(s => s.Type == SleepTypeEnum.SofaBed ||
                                                                             s.Type == SleepTypeEnum.SingleBed) +
                                                         x.Sleeps.Count(s => s.Type != SleepTypeEnum.SofaBed &&
                                                                             s.Type != SleepTypeEnum.SingleBed) * 2);

            hotelRoomPrices = hotelRoomPrices
                .Include(x => x.Sleeps)
                .Include(x => x.Facilities).ThenInclude(x => x.Facility);

            //No Check for CheckIn/CheckOutDate because this is a Test!
            var foundHotels = hotelRoomPrices.GroupBy(x => x.HotelId).Select(x =>
                new HotelAvailable()
                {
                    Hotel = _dbContext.Hotels.Include(x => x.Facilities.Where(x => x.Facility.Highlighted &&
                            (!x.SearchMatchAdult.HasValue || x.SearchMatchAdult.Value == request.AdultCount) &&
                            (!x.SearchMatchChild.HasValue || x.SearchMatchChild.Value == childCount)))
                        .ThenInclude(x => x.Facility)
                        .FirstOrDefault(h => h.Id == x.First().HotelId)!,
                    //Hotel = x.First().Hotel,
                    FirstAvailableRoom = x.OrderBy(r => r.Price).FirstOrDefault()
                });
            return foundHotels;
        }

        public async Task<HotelWithNeighbourhood> GetHotel(long hotelId)
        {
            var hotel = (await _dbContext.Hotels.Include(x=>x.Facilities).ThenInclude(x=>x.Facility).Include(x=>x.PaymentCurrency).FirstOrDefaultAsync(x => x.Id == hotelId))!;
            if (hotel == null) return null;
            var latLonDif = GetLatLonDif(1000);
            var maxLat = hotel.Latitude + latLonDif;
            var minLat = hotel.Latitude - latLonDif;
            var maxLon = hotel.Longitude + latLonDif;
            var minLon = hotel.Longitude - latLonDif;

            var neighBours = _dbContext.Neighbourhoods.Where(x =>
                x.CenterLatitude >= minLat && x.CenterLatitude <= maxLat && x.CenterLongitude >= minLon &&
                x.CenterLongitude <= maxLon);
            return new HotelWithNeighbourhood()
            {
                Hotel = hotel,
                Neighbourhoods = neighBours
            };
        }

        public async Task<IEnumerable<HotelRoomPrice>> GetHotelRooms(RoomRequest request)
        {
            var hotelRoomPrices = _dbContext.HotelRoomPrices.Include(x => x.Discounts).Include(x=>x.CancellationPolicies).Include(x=>x.Facilities).ThenInclude(x=>x.Facility).Where(x => x.HotelId == request.HotelId);


            hotelRoomPrices = _dbContext.HotelRoomPrices
                .Where(x => x.LeftCount >= request.RoomCount);

            //No Strict Check for Child Age, because this is a Test!
            var childCount = request.ChildAges.Count(x => x >= 2);
            var totalCount = request.AdultCount + childCount;
            hotelRoomPrices = hotelRoomPrices.Where(x => totalCount ==
                                                         x.Sleeps.Count(s => s.Type == SleepTypeEnum.SofaBed ||
                                                                             s.Type == SleepTypeEnum.SingleBed) +
                                                         x.Sleeps.Count(s => s.Type != SleepTypeEnum.SofaBed &&
                                                                             s.Type != SleepTypeEnum.SingleBed) * 2);

            hotelRoomPrices = hotelRoomPrices
                .Include(x => x.Sleeps)
                .Include(x => x.Facilities).ThenInclude(x => x.Facility);
            hotelRoomPrices = hotelRoomPrices.Include(x => x.CancellationPolicies);
            //No Check for CheckIn/CheckOutDate because this is a Test!
            return hotelRoomPrices.OrderBy(x => x.Price);
        }

        public async Task<IEnumerable<Currency>> GetCurrencies()
        {
            return _dbContext.Currencies.Where(x => !string.IsNullOrWhiteSpace(x.Name));
        }

        public static decimal GetLatLonDif(double distance)
        {
            var latDif = (decimal)(0.0001D * distance);
            return latDif;
        }
    }

}
