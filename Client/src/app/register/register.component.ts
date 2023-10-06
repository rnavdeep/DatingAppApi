import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from '../services/account.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Input() usersFromHomeComponent:any;
  @Output() cancelRegister = new EventEmitter();

  model: any={};

  constructor(private accountService:AccountService, private router:Router,private toastr:ToastrService){}

  ngOnInit():void{}

  register(){
    this.accountService.register(this.model).subscribe({
      next:resp=>{
          console.log(resp);
          this.cancel();
      },error:err=>{this.toastr.error("Inavlid LoginName or Password")}
    }
  )
    console.log(this.usersFromHomeComponent);
  }
  cancel(){
    this.cancelRegister.emit(false);
    this.router.navigateByUrl('/');
  }
}
