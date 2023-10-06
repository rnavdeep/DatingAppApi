import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../services/account.service';
import { map } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  constructor(private http:HttpClient, private router:Router,public accountService:AccountService,private toastr:ToastrService){}

  ngOnInit():void{

  }
  registerToggle(){
    this.router.navigateByUrl("/register");
  }
  loginToggle(){
    this.router.navigateByUrl("/login");
  }
}
