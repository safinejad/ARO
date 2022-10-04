import {APP_INITIALIZER, NgModule} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import {HTTP_INTERCEPTORS, HttpClient, HttpClientModule} from "@angular/common/http";
import {applicationInitializerFactory} from "./applicationInitializerFactory";
import {AppConfigService} from "../services/app-config-service";
import {AuthInterceptor} from "../services/auth.interceptor";
import {ActivatedRoute, RouterModule} from "@angular/router";
import {HotelItemComponent} from "./components/hotel-item/hotel-item.component";

@NgModule({
  declarations: [
    AppComponent,
    HotelItemComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule

  ],
  providers: [{
    provide: APP_INITIALIZER,
    useFactory: applicationInitializerFactory,
    deps: [AppConfigService],
    multi: true
  },    {
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }

