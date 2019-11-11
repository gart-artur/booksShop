import { CanActivate, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { Injectable } from '@angular/core';


@Injectable()
export class AuthGuard implements CanActivate {
    constructor(private router: Router,
        private _authService: AuthService) { }

    async canActivate() {
        const isAuthenticated: boolean = await this._authService.isAuthenticated().then(res => { return res });
        if (isAuthenticated) {           
            return true;
        }
        this.router.navigate(['auth', 'login'], { replaceUrl: true });
        return false;
    }
}
