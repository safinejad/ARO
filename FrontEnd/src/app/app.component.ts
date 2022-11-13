import {AfterViewInit, Component, OnDestroy} from '@angular/core';
import {BookingService} from "../services/booking.service";
import {AvailableDto, CurrencyDto} from "../models/available-dto";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent  implements AfterViewInit, OnDestroy{
  constructor() {
  }
  ngAfterViewInit(): void {
  }

  ngOnDestroy(): void {
 }
}
