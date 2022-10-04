import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import {AppSettings, Globals} from "../@types/configs";

@Injectable(
  { providedIn: 'root' }
)
export class AppConfigService {

  loaded = false;

  constructor(private http: HttpClient) {
  }

  loadConfig(): Promise<void> {
    return this.http
      .get<AppSettings>('/assets/app.config.json')
      .toPromise()
      .then(data => {
        Globals.AppSettings = data as AppSettings;
        this.loaded = true;
      });
  }

}

