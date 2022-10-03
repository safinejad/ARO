using AutoMapper;
using Contracts.DataModel;
using Contracts.ObjectModel;

namespace API.Dtos;

public class BookingProfile : Profile
{
    public BookingProfile()
    {
        CreateMap<Hotel, HotelDto>()
            .ForMember(dst => dst.DistrictId,
                opt => opt.MapFrom(
                    src => src.GeographicBoundary.GetBoundary(GeographicBoundaryTypeEnum.District).Id))
            .ForMember(dst => dst.DistrictName,
                opt => opt.MapFrom(src =>
                    src.GeographicBoundary.GetBoundary(GeographicBoundaryTypeEnum.District).Name))
            .ForMember(dst => dst.HighlightedFacilities,
                opt => opt.MapFrom(src => src.Facilities.Select(x => x.Facility).Where(x => x.Highlighted)));

        CreateMap<Facility, FacilityDto>();

        CreateMap<HotelAvailable, HotelAvailableDto>()
            .ForMember(dst => dst.Hotel, opt => opt.MapFrom(src => src.Hotel))
            .ForMember(dst => dst.FirstRoomAvailable, opt => opt.MapFrom(src => src.FirstAvailableRoom));

        CreateMap<HotelRoomPrice, RoomAvailableDto>()
            .ForMember(dst => dst.Sleeps, opt => opt.MapFrom(src => src.Sleeps))
            .ForMember(dst => dst.HasFreeCancellation, opt =>
                opt.MapFrom(src => src.CancellationPolicies.OrderBy(x => x.DayBeforeCheckOut).FirstOrDefault(x =>
                    (!x.CashCharge.HasValue || x.CashCharge.Value < 1) &&
                    (!x.PercentCharge.HasValue || x.PercentCharge.Value < 1))))
            .ForMember(dst => dst.DiningFacility,
                opt =>
                    opt.MapFrom(src => src.Facilities.FirstOrDefault(x =>
                        x.Facility.FacilityType == FacilityTypeEnum.Dining &&
                        (!x.ExtraCharge.HasValue || x.ExtraCharge.Value < 1) &&
                        (!x.ExtraChargePerPerson.HasValue || x.ExtraChargePerPerson.Value < 1)).Facility));



        CreateMap<Sleep, SleepDto>();
        CreateMap<Facility, FacilityDto>();
    }
}