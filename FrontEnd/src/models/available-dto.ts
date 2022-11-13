export interface AvailableDto
{
  readonly hotel :HotelAvailableDto,
  readonly firstRoomAvailable :RoomAvailableDto,

}
export interface HotelWithNeighbourhoodDto{
  readonly hotel :HotelDto,
  readonly neighbourhoods :NeighbourhoodDto[],
}
export interface NeighbourhoodDto{
  id:number,
  name :string,
  centerLatitude :number,
  centerLongitude :number,
  propertyType :PropertyTypeEnum,
  distanceFromHotel :number
}
export interface HotelDto {
  id: number,
  name: string,
  stars: number,
  latitude: number,
  longitude: number,
  description: string,
  district: DistrictDto,
  paymentCurrency: CurrencyDto,
  facilities: HotelFacilityDto,
  sustainableBadge: boolean,
  promoted: boolean,
  overallScore: number,
  numberOfReviews: number,
  comfortRate: number,
  staffRate: number,
  facilityRate: number,
  cleanlinessRate: number,
  locationRate: number,
  valueRate: number,
  maxRoomsInReservation: number,
}
export interface DistrictDto {
  id: number,
  name: string,
  mapLink: string,
  centerLatitude: number,
  centerLongitude: number,
  distanceFromHotel: number
}
export interface HotelFacilityDto {
  id: number,
  name: string,
  facilityType: FacilityTypeEnum,
  extraChargeRequired: boolean,
}
export interface HotelAvailableDto {
  id: number,
  name: string,
  stars: number,
  addressBaseLine: string,
  districtName: string,
  districtId: number,
  highlightedFacilities: FreeFacilityDto[],
  sustainableBadge: boolean,
  promoted: boolean,
  overallScore: number,
  numberOfReviews: number,
  comfortRate: number
}
export interface  CurrencyDto {
  name: string,
  sign: string
}
export interface RoomAvailableDto {
 price: number,
 priceTax:number,
 sleeps: SleepDto[],
 hasFreeCancellation: boolean,
 diningFacility: FreeFacilityDto,
 payBackCredit : number,
 leftCount : number
}
export interface SleepDto{
  type: SleepTypeEnum
  count: number
}
export enum SleepTypeEnum{
  SingleBed,
  DoubleBed,
  KingDouble,
  QueenDouble,
  SofaBed,
  NoBed
}
export enum PropertyTypeEnum{
  Airport,
  Museum,
  Restaurant,
  Nature,
  Beach,
  ShoppingCenter,
  TopAttraction,
  PublicTransport,
}
export interface FreeFacilityDto {
  id: number,
  name: string,
  facilityType: FacilityTypeEnum
}
export enum FacilityTypeEnum{
  Transfer,
  Accommodation,
  Dining,
  Luxury,
  Location,
  Pricing,
  Languages,
  Safety,
  Parking,
  Internet,
  Fitness,
  Entertainment,
  Outdoors,
  Reception,
  CleaningServices,
  Comfort,
  Recommend,
  Floor
}
