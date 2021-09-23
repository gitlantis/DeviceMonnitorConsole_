import { Injectable } from '@angular/core';
import { User } from '../helpers/auth/user.model';

import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Constants } from 'src/constants';
import { BaseService } from './base.service';
import { UserService } from './user.service';
import { ToastrService } from 'ngx-toastr';
@Injectable({
  providedIn: 'root'
})
export class AuthService extends BaseService {

  formData: User = new User();

  constructor(httpClient: HttpClient, router: Router, constants: Constants, userService: UserService, private toastr: ToastrService){
    super(httpClient, router, constants, userService)
  }

  login(formData) {
    return this.httpClient.post(this.constants.baseUrl + '/User/Login', formData, { headers: this.header }).subscribe(
      res => {
        if (res != null) {
          this.userService.setUsername(res['username']);
          this.userService.setToken(res['token'])
          this.router.navigateByUrl('/home');
          return true;
        } else {          
          this.toastr.error("Check username and password again", 'Auth error!');     
          return false;
        }
      },
      err => {   
        this.toastr.error(err.error.message, 'Auth error!');     
        console.log(err)
        return err;
      });
  }

  logout() {
    this.userService.removeToken();
    this.router.navigateByUrl('/login');
    window.location.reload();
  }
}
