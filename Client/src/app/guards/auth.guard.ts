import { CanActivateFn } from '@angular/router';
import { AccountService } from '../services/account.service';
import { inject } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { map } from 'rxjs';

export const AuthGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);//inject service to see if user is logged in or not
  const toartService = inject(ToastrService);//inject toaster service for message to user

  /**Check using account service is user is logged in, if yes return true, if not return false and show ui message */
  return accountService.currentUsers$.pipe(
    map(user=>{
      if(user) return true;
      else{
        toartService.error("Log in to view page.")
        console.log("login");
        return false;
      }
    })
  )
};
