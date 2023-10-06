import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { User } from '../models/user';
import { AccountService } from '../services/account.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  model: any = {}
  currentUser$: Observable<User | null> = of(null);
  constructor(public accountService:AccountService, public router:Router,private toast:ToastrService){ }

  ngOnInit(): void {
    this.currentUser$ = this.accountService.currentUsers$;
    this.accountService.currentUsers$.subscribe({next:resp=>{
      this.model.username = resp?.userName;
    }});
  }


  login(){
    console.log(this.model);
    this.accountService.login(this.model).subscribe({
      next: response =>{
        this.toast.success(this.model.username+" logged in.")
        this.router.navigateByUrl("/members");
      },
      error: err =>{
        var error = err.statusText;
        if(error!=null){
          this.toast.error(err.statusText);
        }else{
          this.router.navigateByUrl("/");
        }
      }
    })
  }

  logout(){
    this.accountService.logout();
    this.router.navigateByUrl("/");
  }
  cancel(){
    this.router.navigateByUrl("/");

  }
}
