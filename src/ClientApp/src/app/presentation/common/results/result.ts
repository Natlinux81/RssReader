export interface Result <T = any> {
  isSuccess: boolean;
  isFailure: boolean;
  error?: Error;
  value?: T;
}
