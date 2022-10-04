import {AppConfigService} from "../services/app-config-service";

export function applicationInitializerFactory(appConfig: AppConfigService) {

  return () => appConfig.loadConfig();

}
