import {Component, Input, OnInit} from "@angular/core";
import {
  AvailableDto,
  CurrencyDto,
  FacilityTypeEnum,
  FreeFacilityDto, HotelWithNeighbourhoodDto,
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
  public hotelWithNeighbour: HotelWithNeighbourhoodDto;
  private rooms: AvailableDto[];


    constructor(private bookingService: BookingService, private router: Router ) {
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

      this.bookingService.getHotel(id).subscribe(x=> this.hotelWithNeighbour = x);
      this.bookingService.getHotelRooms(id).subscribe(x=> this.rooms = x);
    }

  getCount(sleeps: SleepDto[]) {
    return sleeps.filter(x=>x.type == SleepTypeEnum.SingleBed || x.type == SleepTypeEnum.SofaBed).length +
      (sleeps.filter(x=>x.type != SleepTypeEnum.SingleBed && x.type != SleepTypeEnum.SofaBed).length * 2);
  }

  getFacilities(highlightedFacilities: FreeFacilityDto[], type: FacilityTypeEnum[]) {
    return  highlightedFacilities.filter(x=> type.indexOf(x.facilityType) >= 0 );
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
