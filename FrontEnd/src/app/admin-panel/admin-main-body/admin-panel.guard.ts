import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
} from '@angular/router';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

import { AuthorizationService } from '../../services/authorization.service';

import AuthHelper from '../../utils/authHelper';

@Injectable()
export class AdminPanelGuard
  implements CanActivate
{

  constructor(
    private authService: AuthorizationService,
    private router: Router
  ) {
    
  }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> | boolean {
    if (this.authService.getIsAdmin() == false) {
      this.router.navigate(['']);
      return false;
    }
    return true;
  }

}
