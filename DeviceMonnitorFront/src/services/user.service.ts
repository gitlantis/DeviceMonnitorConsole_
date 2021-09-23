import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor() { }
  
  getToken() {
    return localStorage.getItem('saltekToken');
  }

  setToken(token: string) {    
    localStorage.setItem('saltekToken', token);
  }

  removeToken() {
           localStorage.removeItem('userName');
    return localStorage.removeItem('saltekToken');
  }

  getUsername() {    
    return localStorage.getItem('userName');
  }

  setUsername(username: string) {    
    localStorage.setItem('userName', username);
  }
}
