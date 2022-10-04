import { HttpClient } from '@angular/common/http';
import { setValue, getValue } from '../helpers/data.store';
import { LinkGenerator } from './link-generator.service';

export class ServiceBase {
  constructor(httpClient: HttpClient, linkGenerator: LinkGenerator) {
    setValue(this, "httpClient", httpClient);
    setValue(this, "linkGenerator", linkGenerator);
  }

  get linkGenerator(): LinkGenerator {
    return getValue(this, "linkGenerator");
  }

  get httpClient(): HttpClient {
    return getValue(this, "httpClient");
  }
}
