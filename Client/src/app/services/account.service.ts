import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = 'https://localhost:7028/api/'

  private currentUserSource = new BehaviorSubject<User | null>(null);//union type
  currentUsers$ = this.currentUserSource.asObservable();

  constructor(private http:HttpClient) { }

  login(model:any){
    return this.http.post<User>(this.baseUrl + 'Users/Login', model).pipe(
      map((response:User)=>{
        const user = response;
        if(user){
          localStorage.setItem('user',JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    )
  }
  register(model:any){
    return this.http.post<User>(this.baseUrl + 'Users/Register',model).pipe(
      map((response)=>{
        if(response){
          localStorage.setItem('user',JSON.stringify((response)));
          this.currentUserSource.next(response);
        }
        return response;
      })
    )

  }
  logout(){
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }

  setCurrentUser(user:User){
    this.currentUserSource.next(user);
  }
}
