import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';
import { User } from '../Model/User';
import { ShareService } from './share.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  role: string;

  userData = new BehaviorSubject<User>(new User());

  constructor(private http: HttpClient, private router: Router, private share: ShareService) {
  }

  login(userDetails) {
    return this.http.post<any>('/api/auth/Login', userDetails) // url controllerului pentru login
      .pipe(map(response => {
        localStorage.setItem('authToken', response.token);
        this.setUserDetails();
        return response;
      }));
  }

  setUserDetails() {
    if (localStorage.getItem('authToken')) {
      let userDetails = new User();
      const decodeUserDetails = JSON.parse(window.atob(localStorage.getItem('authToken').split('.')[1]));
      userDetails.id = decodeUserDetails.nameid;
      userDetails.userName = decodeUserDetails.unique_name;
      userDetails.email = decodeUserDetails.email;
      this.role = decodeUserDetails.role;
      userDetails.role = decodeUserDetails.role;
      this.userData.next(userDetails);
    }
  }

  getRole() {
    return this.role;
  }

  logout() {
    localStorage.removeItem('authToken');
    this.router.navigate(['/']);
    this.userData.next(new User());
  }
}
