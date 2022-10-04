import {Component, Input, OnInit} from "@angular/core";
import {AvailableDto, SleepDto, SleepTypeEnum} from "../../../models/available-dto";


declare function escape(s: string): string;
@Component({
  selector: "app-hotel-item",
  templateUrl: "./hotel-item.component.html",
  styleUrls: ["./hotel-item.component.scss"]
})
export class HotelItemComponent implements OnInit {
    @Input() hotelData: AvailableDto;

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
}
