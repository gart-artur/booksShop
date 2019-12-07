import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { LoginModel } from '../models/login-model';
import { RegisterService } from '../services/register.service';
import { JwtView } from '../models/JwtView';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;

  constructor(private _formBuilder: FormBuilder,
    private _accountService: RegisterService,
    private _authService: AuthService,
    private _router: Router) { }

  ngOnInit() {
    this._authService.logout();
    this.loginForm = this._formBuilder.group({
      email: [''],
      password: ['']
    });
  }

  login() {
    let loginModel = new LoginModel();
    loginModel.email = this.loginForm.controls['email'].value;
    loginModel.password = this.loginForm.controls['password'].value;

    this._accountService.login(loginModel).subscribe((response: JwtView) => {
      this._authService.login(response);
      localStorage.setItem('currentUser',JSON.stringify(response));
      this._router.navigate(['books']);
    });
  }

}
