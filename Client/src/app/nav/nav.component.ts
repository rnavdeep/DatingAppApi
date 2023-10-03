import { Component, OnInit } from '@angular/core';
import { AccountService } from '../services/account.service';
import { JsonPipe } from '@angular/common';
import { Observable, map, of } from 'rxjs';
import { User } from '../models/user';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {}
  currentUser$: Observable<User | null> = of(null);
  constructor(public accountService:AccountService){ }

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
        console.log(response);
      },
      error: err =>{
        console.log(err);
      }
    })
  }

  logout(){
    this.accountService.logout();
  }
}
