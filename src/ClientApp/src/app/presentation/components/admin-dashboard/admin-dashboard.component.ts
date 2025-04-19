import {Component, inject, OnInit} from '@angular/core';
import {ApiService} from "../../../infrastructure/services/api.service";
import {NgFor} from "@angular/common";

@Component({
  selector: 'app-admin-dashboard',
  imports: [
    NgFor
  ],
  templateUrl: './admin-dashboard.component.html',
  standalone: true,
  styleUrl: './admin-dashboard.component.scss'
})
export class AdminDashboardComponent implements OnInit {
  public users: any = [];
  private apiService = inject(ApiService)

  ngOnInit(): void {
    this.apiService.getAllUsers().subscribe(result => {
      this.users = result;
      console.log(
        'user', result);
    })
  }

}
