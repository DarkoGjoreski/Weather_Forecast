import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(private router: Router) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        if (localStorage.getItem('AccessToken')) {
            return true;
        }

        localStorage.clear();
        this.router.navigate(['/home'], { queryParams: { returnUrl: state.url }});
        return false;
    }
}
