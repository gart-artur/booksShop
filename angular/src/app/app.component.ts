import { Component } from '@angular/core';
import { Router, NavigationStart } from '@angular/router';
import { AuthService } from './services/auth.service';

@Component({
    selector: 'app',
    templateUrl: 'app.component.html'
})

export class AppComponent {  
  
    showHead: boolean = false;
  
    ngOnInit() {
    }      
    
    constructor(private router: Router,
      private _authService : AuthService) {
      router.events.forEach(async (event) => {
         if (event instanceof NavigationStart) {
          const isAuthenticated: boolean = await this._authService.isAuthenticated().then(res => { return res })
         if (isAuthenticated && event.url!=='/login'&& event.url!=='/forgot'&& event.url!=='/register') {
          this.showHead = true;
          } else {
            this.showHead = false;
          }
        }
          });
        }
}
