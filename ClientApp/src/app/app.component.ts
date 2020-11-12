import {Component, OnDestroy} from '@angular/core';
import { AuthService } from './services/auth.service';
import { Router, ActivatedRoute, NavigationEnd } from '@angular/router';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(private authService: AuthService, private router: Router, private activatedRoute: ActivatedRoute) {
    if (localStorage.getItem('authToken')) {
      this.authService.setUserDetails();
    }
  }
}
