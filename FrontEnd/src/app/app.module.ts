import {APP_INITIALIZER, NgModule} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import {HTTP_INTERCEPTORS, HttpClient, HttpClientModule} from "@angular/common/http";
import {applicationInitializerFactory} from "./applicationInitializerFactory";
import {AppConfigService} from "../services/app-config-service";
import {ReqInterceptor} from "../services/req-interceptor.service";
import {ActivatedRoute, RouterModule, Routes} from "@angular/router";
import {HotelItemComponent} from "./components/hotel-item/hotel-item.component";
import {HotelDetailComponent} from "./components/hotel-detail/hotel-detail.component";
import {AvailableComponent} from "./components/available/available.component";
import {MapComponent} from "./components/map/map.component";
import {MapMarkerDirective} from "./components/map/map-marker/map-marker.directive";
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
export class AppRoutingModule { }

@NgModule({
  declarations: [
    AppComponent,
    HotelItemComponent,
    AvailableComponent,
    HotelDetailComponent,
    MapComponent,
    MapMarkerDirective
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot([
      {path: '', redirectTo:'available', pathMatch: 'full'},
      { path: 'available', component: AvailableComponent},
      { path: 'hotel', component: HotelDetailComponent, children: [
          { path: '**', component: HotelDetailComponent}
        ] }
    ]),
    HttpClientModule,
    NoopAnimationsModule
  ],
  exports: [RouterModule,    MapComponent,
    MapMarkerDirective],
  providers: [{
    provide: APP_INITIALIZER,
    useFactory: applicationInitializerFactory,
    deps: [AppConfigService],
    multi: true
  },    {
    provide: HTTP_INTERCEPTORS,
    useClass: ReqInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }

