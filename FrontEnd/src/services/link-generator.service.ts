import { Injectable } from '@angular/core';
import { getOrCreateValue } from '../helpers/data.store';
import {Globals} from "../@types/configs";


@Injectable({ providedIn: 'root' })
export class LinkGenerator {

  get apiBaseUrl(): string {
    return Globals.AppSettings.apiUrl;
  }

  url(subpath: string): string {
    if (subpath[0] == '/') {
      subpath = subpath.substr(1);
    }

    return `${this.apiBaseUrl}/${subpath}`;
  }

}
