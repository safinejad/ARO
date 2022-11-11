export interface AvailableDto
{
  readonly hotel :HotelAvailableDto,
  readonly firstRoomAvailable :RoomAvailableDto,

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
