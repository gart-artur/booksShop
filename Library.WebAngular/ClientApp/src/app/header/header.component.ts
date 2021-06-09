import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { LoadService } from '../services/load.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor(
    private authService: AuthService,
    private loadService: LoadService) { }

  ngOnInit() {
  }

  get isLoading(): boolean {
    return this.loadService.isLoading;
  }

  async logout() {
    await this.authService.logout();
  }
}
