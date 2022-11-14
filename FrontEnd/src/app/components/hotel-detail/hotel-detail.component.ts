import {Component, Input, OnInit} from "@angular/core";
import {
  AvailableDto,
  CurrencyDto,
  FacilityTypeEnum,
  FreeFacilityDto, HotelDto, HotelWithNeighbourhoodDto, RoomDto,
  SleepDto,
  SleepTypeEnum
} from "../../../models/available-dto";
import {BookingService} from "../../../services/booking.service";
import {Router} from "@angular/router";


declare function escape(s: string): string;
@Component({
  selector: "app-hotel-detail",
  templateUrl: "./hotel-detail.component.html",
  styleUrls: ["./hotel-detail.component.scss"]
})
export class HotelDetailComponent implements OnInit {
  public currency: CurrencyDto;
  public currencies: CurrencyDto[];
  facilitiesOpen = false;
  public hotelWithNeighbour: HotelWithNeighbourhoodDto;
  public rooms: RoomDto[];
  public _inProgressHotel: boolean;
  public _inProgressRooms: boolean;
  public isDiscriptionExpanded = false;
  get
  inProgress(): boolean{
    return this._inProgressHotel || this._inProgressRooms
  }
  toggleFacilities() {
    this.facilitiesOpen = !this.facilitiesOpen;
  }


    constructor(private bookingService: BookingService, private router: Router ) {
    this._inProgressHotel = true;
    }
    public get hotelDetails(): HotelDto{
        return this.hotelWithNeighbour?.hotel;
    }
    ngOnInit() {
      let url = (this.router.url as any).trimEnd('/');
      let idPart=url.substr(url.lastIndexOf('/')+1);
      let id = 0;
      if (!isNaN(+idPart)  ){
        id = +idPart
      }else{
        return;
      }
      this._inProgressHotel = true;
      this._inProgressRooms = true;
      this.bookingService.getHotel(id).subscribe(x=> {
        this.hotelWithNeighbour = x;
        this._inProgressHotel = false;
      });
      this.bookingService.getHotelRooms(id).subscribe(x=> {
        this.rooms = x;
        this._inProgressRooms = false;
      });
      this.bookingService.getCurrencies().subscribe(value => {
        this.currencies = value;
        if (this.bookingService.userCurrency) {
          this.currency = this.currencies.filter(value => value && value.name == this.bookingService.userCurrency)[0];
        }else{
          this.currency = this.currencies[0];
        }

      });
    }

  getCount(sleeps: SleepDto[]) {
    return sleeps.filter(x=>x.type == SleepTypeEnum.SingleBed || x.type == SleepTypeEnum.SofaBed).length +
      (sleeps.filter(x=>x.type != SleepTypeEnum.SingleBed && x.type != SleepTypeEnum.SofaBed).length * 2);
  }

  getFacilities(highlightedFacilities: FreeFacilityDto[], type: FacilityTypeEnum[]) {
    return  highlightedFacilities.filter(x=> type.indexOf(x.facilityType) >= 0 );
  }
  grad(){
    this.isDiscriptionExpanded = !this.isDiscriptionExpanded;
  }

  getTypes(sleeps: SleepDto[]) {
    let single = sleeps.filter(x => x.type == SleepTypeEnum.SingleBed).length;
    let double = sleeps.filter(x => x.type == SleepTypeEnum.DoubleBed).length;
    let queen = sleeps.filter(x => x.type == SleepTypeEnum.QueenDouble).length;
    let king = sleeps.filter(x => x.type == SleepTypeEnum.KingDouble).length;
    return (single ? single + ' S ' : '') +
      (double ? double + ' D ' : '') +
      (queen ? queen + ' QD ' : '') +
      (king ? king + ' KD ' : '');
  }

}
