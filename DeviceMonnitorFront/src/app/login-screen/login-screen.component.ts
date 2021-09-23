import { Component, OnInit } from '@angular/core';
import {NgForm} from '@angular/forms';
import { AuthService } from 'src/services/auth.service';
@Component({
  selector: 'app-login-screen',
  templateUrl: './login-screen.component.html',
  styles: [
  ]
})
export class LoginScreenComponent implements OnInit {

  constructor(public authService: AuthService) { }

  ngOnInit(): void {    
  }

  onSubmit(form: NgForm) {        
    var res = this.authService.login(form.value)               
  }

}
