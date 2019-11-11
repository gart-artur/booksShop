import { Component, OnInit } from '@angular/core';
import { RegisterViewModel } from '../models/register-view-model';
import { RegisterService } from '../services/register.service';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm:FormGroup;
  user = new RegisterViewModel;

  constructor(private _registerService:RegisterService,
    private formBuilder : FormBuilder ) { }

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      userName:['',Validators.required],
      email:['',[Validators.required,Validators.email]],
      password:['',[Validators.required,Validators.minLength(6)]],
      passwordConfirm:['',[Validators.required,Validators.minLength(6)]]  

    })
  }

  register(){    
    if(this.registerForm.valid && this.registerForm.value.password==this.registerForm.value.passwordConfirm){
      this.user.userName = this.registerForm.value.userName,
      this.user.email = this.registerForm.value.email,
      this.user.password = this.registerForm.value.password,
      this.user.passwordConfirm = this.registerForm.value.passwordConfirm          
      this._registerService.registerModel(this.user).subscribe(() => {
        window.alert("User successed created!");
      });
      
    }
    else{
    window.alert("Something wrong...try again.") 
    }
      
  }
}
