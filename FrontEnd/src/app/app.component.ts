import {AfterViewInit, Component, OnDestroy} from '@angular/core';
import {BookingService} from "../services/booking.service";
import {AvailableDto, CurrencyDto} from "../models/available-dto";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent  implements AfterViewInit, OnDestroy{
  title = 'FrontEnd';
  public availables: AvailableDto[] = [];
  public currencies: CurrencyDto[];
  public currency: CurrencyDto;
  constructor(private bookingService: BookingService) {
  }
  ngAfterViewInit(): void {
    this.bookingService.getCurrencies().subscribe(value => {
      this.currencies = value;
      if (this.bookingService.userCurrency) {
        this.currency = this.currencies.filter(value => value && value.name == this.bookingService.userCurrency)[0];
      }else{
        this.currency = this.currencies[0];
      }

    });
    this.reAvailable();
  }

  ngOnDestroy(): void {
  }

  changeCurrency(elem: any) {
    let name = elem.value;
    this.currency = this.currencies.filter(value => value && value.name == name)[0];
    this.bookingService.userCurrency = this.currency.name;

    this.reAvailable();
  }

  private reAvailable() {
    this.bookingService.getAvailable().subscribe(value => {
      this.availables = value;
    });
  }
}
