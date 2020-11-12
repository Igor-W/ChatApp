import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent implements OnInit {

  loading = false;
  loginForm: FormGroup;
  submitted = false;
  returnUrl: string;
  userURl: string = '/api/user-panel';
  adminURL: string = '/api/admin-panel';
  role: string;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService) { }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  get loginFormControl() {
    return this.loginForm.controls;
  }


  onSubmit() {
    this.submitted = true;
    if (this.loginForm.invalid) {
      return;
    }

    this.loading = true;
    let returnUrl;


    this.authService.login(this.loginForm.value)
      .pipe(first())
      .subscribe(
        () => {
          if (this.authService.getRole() === 'Admin') {
            returnUrl = this.route.snapshot.queryParamMap.get('returnUrl') || this.adminURL;
          } else {
            returnUrl = this.route.snapshot.queryParamMap.get('returnUrl') || this.userURl;
          }
          this.router.navigate([returnUrl]);
        },
        () => {
          this.loading = false;
          this.loginForm.reset();
          this.loginForm.setErrors({
            invalidLogin: true
          });
        });
  }

}
