using System.Globalization;
using AutoMapper;
using Contracts.DataModel;
using Contracts.ObjectModel;

namespace API.Dtos;

public class BookingProfile : Profile
{
    public BookingProfile()
    {
        CreateMap<Hotel, HotelAvailableDto>()
            .ForMember(dst => dst.DistrictId,
                opt => opt.MapFrom(
                    src => src.GeographicBoundary.GetBoundary(GeographicBoundaryTypeEnum.District).Id))
            .ForMember(dst => dst.DistrictName,
                opt => opt.MapFrom(src =>
                    src.GeographicBoundary.GetBoundary(GeographicBoundaryTypeEnum.District).Name))
            .ForMember(dst => dst.HighlightedFacilities,
                opt => opt.MapFrom(src => src.Facilities.Where(x => x.Facility.Highlighted).Select(x=>x.Facility)));
        CreateMap<Facility, FreeFacilityDto>();
        //CreateMap<HotelFacility, HotelFacilityDto>()
        //    .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Facility.Id))
        //    .ForMember(dst => dst.FacilityType, opt => opt.MapFrom(src => src.Facility.FacilityType))
        //    .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Facility.Name));

        CreateMap<HotelAvailable, AvailableDto>()
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

        CreateMap<Currency, CurrencyDto>();
        CreateMap<Neighbourhood, NeighbourhoodDto>();

        CreateMap<HotelWithNeighbourhood, HotelWithNeighbourhoodDto>();
        CreateMap<Hotel, HotelDto>()
            .ForMember(dst => dst.District, opt => opt.MapFrom(src => src.GeographicBoundary))
            .ForMember(dst => dst.Facilities, opt => opt.MapFrom(src => src.Facilities));
        CreateMap<HotelFacility, HotelFacilityDto>()
            .ForMember(src => src.FacilityType, opt => opt.MapFrom(dst => dst.Facility.FacilityType))
            .ForMember(src => src.Name, opt => opt.MapFrom(dst => dst.Facility.Name))
            .ForMember(src => src.Id, opt => opt.MapFrom(dst => dst.Facility.Id))
            .ForMember(src => src.ExtraChargeRequired, opt => opt.MapFrom(dst => dst.ExtraChargeRequired));
        CreateMap<GeographicBoundary, DistrictDto>();

        CreateMap<HotelRoomPrice, RoomDto>()
            .ForMember(src => src.Facilities, opt => opt.MapFrom(dst => dst.Facilities));
        CreateMap<CancellationPolicy, CancellationPolicyDto>();
        CreateMap<RoomFacility, RoomFacilityDto>()
            .ForMember(src => src.FacilityType, opt => opt.MapFrom(dst => dst.Facility.FacilityType))
            .ForMember(src => src.Name, opt => opt.MapFrom(dst => dst.Facility.Name))
            .ForMember(src => src.Id, opt => opt.MapFrom(dst => dst.Facility.Id))
            .ForMember(src => src.ExtraCharge, opt => opt.MapFrom(dst => dst.ExtraCharge))
            .ForMember(src => src.ExtraChargePerPerson, opt => opt.MapFrom(dst => dst.ExtraChargePerPerson))
            .ForMember(src => src.FromDate, opt => opt.MapFrom(dst => dst.FromDate))
            .ForMember(src => src.ToDate, opt => opt.MapFrom(dst => dst.ToDate));


    }
}