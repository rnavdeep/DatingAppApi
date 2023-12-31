import { Component, OnInit } from '@angular/core';
import { AccountService } from '../services/account.service';
import { JsonPipe } from '@angular/common';
import { Observable, map, of } from 'rxjs';
import { User } from '../models/user';
import { Router } from '@angular/router';
import {ToastrService} from 'ngx-toastr'

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
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
        }
        this.router.navigateByUrl("/");
      }
    })
  }

  logout(){
    this.accountService.logout();
    this.router.navigateByUrl("/");

  }
}
