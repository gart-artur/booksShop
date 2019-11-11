import { Component, OnInit } from '@angular/core';
import { RegisterService } from '../services/register.service';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ChangeEmailView } from '../models/changeEmailView';

@Component({
  selector: 'app-cahnge-email',
  templateUrl: './cahnge-email.component.html',
  styleUrls: ['./cahnge-email.component.css']
})
export class CahngeEmailComponent implements OnInit {

  changeForm : FormGroup;
  constructor(private _accountService:RegisterService,
    private _changeBuilder:FormBuilder) { }

  ngOnInit() {
    this.changeForm= this._changeBuilder.group({
      newEmail:['']
    });
    
  }

  changeEmail(){
    let model = new ChangeEmailView();
    model.newEmail = this.changeForm.controls['newEmail'].value
    model.email = localStorage.getItem('currentEmail')
    this._accountService.changeEmail(model).subscribe(()=>
    {
      window.alert("Chek your new Email.")
    })
  }


}
