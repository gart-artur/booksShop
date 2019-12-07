import { Component, OnInit } from '@angular/core';
import { RegisterService } from '../services/register.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ChangePasswordView } from '../models/changePasswordView';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {

  model = new ChangePasswordView;
  changePasswordForm: FormGroup

  constructor(private _accountService: RegisterService,
    private _formBuilder: FormBuilder) { }

  ngOnInit() {
    this.changePasswordForm = this._formBuilder.group({
      currentPassword: [''],
      newPassword: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required, Validators.minLength(6)]]
    })
  }

  changePassword() {
    if (this.changePasswordForm.valid && this.changePasswordForm.value.newPassword == this.changePasswordForm.value.confirmPassword) {
      this.model.currentPassword = this.changePasswordForm.controls['currentPassword'].value
      this.model.newPassword = this.changePasswordForm.controls['newPassword'].value
      let currentEmail = JSON.parse(localStorage.getItem("currentUser"))
      this.model.email = currentEmail.email;

      this._accountService.changePassword(this.model).subscribe(() => {
        window.alert("Success! Your new password: " + this.model.newPassword)
      })

    }
    else {
      window.alert("Incorrect password or invalid form.")
    }
  }

}
