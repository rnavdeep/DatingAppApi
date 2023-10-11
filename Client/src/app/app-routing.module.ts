import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from './guards/auth.guard';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { loginRegisterGuard } from './guards/login-register.guard';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';

const routes: Routes = [
  {path: 'home',component:HomeComponent},
  {path: '',component:HomeComponent},
  {path: 'login',component:LoginComponent,canActivate:[loginRegisterGuard]},
  {path: 'register',component:RegisterComponent,canActivate:[loginRegisterGuard]},
  {path: 'members',component:MemberListComponent,canActivate: [AuthGuard]},
  {path: 'members/:id',component:MemberDetailComponent,canActivate: [AuthGuard]},
  {path: 'lists',component:ListsComponent,canActivate: [AuthGuard]},
  {path: 'not-found',component:NotFoundComponent},
  {path: 'server-error',component:ServerErrorComponent},
  {path: 'messages',component:MessagesComponent,canActivate: [AuthGuard]},
  {path: '**',component:HomeComponent,pathMatch:'full',canActivate: [AuthGuard]},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
