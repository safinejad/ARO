import { Injectable } from '@angular/core';
import { ServiceBase } from './service-base';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { getOrCreateValue } from '../helpers/data.store';
import { BehaviorSubject, Observable } from 'rxjs';
import { LinkGenerator } from './link-generator.service';
import { filter } from 'rxjs/operators';
import {AvailableDto, CurrencyDto, HotelWithNeighbourhoodDto} from "../models/available-dto";
import { CookieService } from 'ngx-cookie-service';
@Injectable({ providedIn: 'root' })
export class BookingService extends ServiceBase {
  private _currency: string;
  constructor(
    httpClient: HttpClient,
    linkGenerator: LinkGenerator, public cookieService: CookieService) {
    super(httpClient, linkGenerator);
  }
  get userCurrency(): string{

    if (!this._currency){
      this._currency = this.cookieService.get("Currency");
    }
    if (!this._currency) {
      return '';
    }
    return this._currency;
  }
  set  userCurrency(value: string){
    this._currency = value;
    this.cookieService.set("Currency", value)
  }






public getAvailable(): Observable<AvailableDto[]> { //Parameters are for demo
  let geographicBoundary = 0;
  let checkIn = '2023-01-01';
  let checkOut = '2023-01-10';
  let roomCount = 1;
  let adultCount = 3;
    return this.httpClient
      .get<AvailableDto[]>(this.linkGenerator.url(`/Available/${geographicBoundary}/${checkIn}/${checkOut}?roomCount=${roomCount}&adultCount=${adultCount}`));

  }
  public getHotelRooms(id: number): Observable<AvailableDto[]> { //Parameters are for demo
    let checkIn = '2023-01-01';
    let checkOut = '2023-01-10';
    let roomCount = 1;
    let adultCount = 3;
    return this.httpClient
      .get<AvailableDto[]>(this.linkGenerator.url(`/Available/${id}/Rooms/${checkIn}/${checkOut}?roomCount=${roomCount}&adultCount=${adultCount}`));

  }
  public getHotel(id: number): Observable<HotelWithNeighbourhoodDto> {
    return this.httpClient
      .get<HotelWithNeighbourhoodDto>(this.linkGenerator.url(`/Available/${id}`));

  }

  getCurrencies() {
    return this.httpClient
      .get<CurrencyDto[]>(this.linkGenerator.url(`/Available/Currencies`));
  }
}
