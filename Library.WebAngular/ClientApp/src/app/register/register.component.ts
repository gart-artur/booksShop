import { Component, OnInit } from '@angular/core';
import { RegisterViewModel } from '../models/register-view-model';
import { RegisterService } from '../services/register.service';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { NotificationService } from '../services/notification.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  user = new RegisterViewModel;

  constructor(
    private _registerService: RegisterService,
    private formBuilder: FormBuilder,
    private route: Router,
    private notificationService: NotificationService) { }

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      userName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      passwordConfirm: ['', [Validators.required, Validators.minLength(6)]]

    });
  }

  register() {
    if (this.registerForm.valid && this.registerForm.value.password === this.registerForm.value.passwordConfirm) {
      this.user.userName = this.registerForm.value.userName,
        this.user.email = this.registerForm.value.email,
        this.user.password = this.registerForm.value.password,
        this.user.passwordConfirm = this.registerForm.value.passwordConfirm;

      this._registerService.registerModel(this.user).subscribe(() => {
        this.notificationService.showSuccess('User created');
        this.route.navigateByUrl('/login');
      });

    } else {
      this.notificationService.showError('Invalid form');
    }

  }
}
