import {Component, Input, OnInit} from "@angular/core";
import {
  AvailableDto,
  CurrencyDto,
  FacilityTypeEnum,
  FreeFacilityDto,
  SleepDto,
  SleepTypeEnum
} from "../../../models/available-dto";


declare function escape(s: string): string;
@Component({
  selector: "app-hotel-item",
  templateUrl: "./hotel-item.component.html",
  styleUrls: ["./hotel-item.component.scss"]
})
export class HotelItemComponent implements OnInit {
    @Input() hotelData: AvailableDto;
    @Input() currency: CurrencyDto;

    constructor() {
    }

    ngOnInit() {

    }
    showDetails(hotel: AvailableDto): void {
            window.open('/Hotel/'+hotel.hotel.id);
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
