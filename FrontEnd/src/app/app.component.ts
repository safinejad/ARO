import {AfterViewInit, Component, OnDestroy} from '@angular/core';
import {BookingService} from "../services/booking.service";
import {AvailableDto} from "../models/available-dto";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent  implements AfterViewInit, OnDestroy{
  title = 'FrontEnd';
  public availables: AvailableDto[] = [];
  constructor(private bookingService: BookingService) {
  }
  ngAfterViewInit(): void {
    this.bookingService.getAvailable().subscribe(value => {
      this.availables = value;
    })
  }

  ngOnDestroy(): void {
  }
}
