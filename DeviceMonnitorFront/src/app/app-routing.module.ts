import { NgModule } from '@angular/core';
import { MainScreenComponent } from './main-screen/main-screen.component';
import { LoginScreenComponent } from './login-screen/login-screen.component';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../services/auth.guard';

const routes: Routes = [
  { path: 'home', component: MainScreenComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginScreenComponent },
  { path: '**', component: MainScreenComponent, canActivate: [AuthGuard] }
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(routes),
  ],
  exports: [RouterModule]

})
export class AppRoutingModule { }
