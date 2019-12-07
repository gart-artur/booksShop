import { Component, OnInit } from '@angular/core';
import { ForgotPasswordView } from '../models/forgot-password-view';
import { RegisterService } from '../services/register.service';
import { FormGroup, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {

  resetForm: FormGroup;
  constructor(private _accountService: RegisterService,
    private _formBuilde: FormBuilder) { }

  ngOnInit() {
    this.resetForm = this._formBuilde.group({
      email: ['']
    });
  }

  resetPassword() {
    let model = new ForgotPasswordView();
    model.email = this.resetForm.controls['email'].value
    this._accountService.forgotPassword(model).subscribe(() => {
      window.alert("New password send to the Email address")
    });
  }


}
