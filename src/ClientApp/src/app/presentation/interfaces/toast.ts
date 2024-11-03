import {TemplateRef} from "@angular/core";

export interface Toast {
  template: TemplateRef<any>;
  classname?: string;
  delay?: number;
}
