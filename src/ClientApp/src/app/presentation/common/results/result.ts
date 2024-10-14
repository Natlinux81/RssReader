import { RssError } from "./Error";

export interface Result <T = any> {
  isSuccess: boolean;
  isFailure: boolean;
  error?: RssError;
  value?: T;
}
