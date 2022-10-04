import { Injectable } from '@angular/core';
import { ServiceBase } from './service-base';
import { HttpClient } from '@angular/common/http';
import { getOrCreateValue } from '../helpers/data.store';
import { BehaviorSubject, Observable } from 'rxjs';
import { LinkGenerator } from './link-generator.service';
import { filter } from 'rxjs/operators';
import {AvailableDto} from "../models/available-dto";

@Injectable({ providedIn: 'root' })
export class BookingService extends ServiceBase {

  constructor(
    httpClient: HttpClient,
    linkGenerator: LinkGenerator) {

    super(httpClient, linkGenerator);
  }




public getAvailable(clientAccountNumber?: string): Observable<AvailableDto[]> {
  let geographicBoundary = 0;
  let checkIn = '2023-01-01';
  let checkOut = '2023-01-10';
  let roomCount = 1;
  let adultCount = 3;
    return this.httpClient
      .get<AvailableDto[]>(this.linkGenerator.url(`/Available/${geographicBoundary}/${checkIn}/${checkOut}?roomCount=${roomCount}&adultCount=${adultCount}`));

  }
}
