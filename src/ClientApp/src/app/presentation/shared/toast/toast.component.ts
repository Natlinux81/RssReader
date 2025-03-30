import {Component, inject} from '@angular/core';
import {ToastService} from "../../../infrastructure/services/toast.service";
import {NgbToast} from "@ng-bootstrap/ng-bootstrap";
import {NgFor} from "@angular/common";

@Component({
  selector: 'app-toast',
  imports: [
    NgbToast,
    NgFor
  ],
  templateUrl: './toast.component.html',
  styleUrls: ['./toast.component.scss']
})
export class ToastComponent {
  toastService = inject(ToastService);
}
