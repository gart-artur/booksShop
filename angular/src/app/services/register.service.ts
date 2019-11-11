import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RegisterViewModel } from '../models/register-view-model';
import { environment } from 'src/environments/environment';
import { JwtView } from '../models/JwtView';
import { LoginModel } from '../models/login-model';
import { Observable } from 'rxjs';
import { ForgotPasswordView } from '../models/forgot-password-view';
import { ChangeEmailView } from '../models/changeEmailView';
import { ChangePasswordView } from '../models/changePasswordView';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {

constructor(private http : HttpClient) { }

registerModel(model :RegisterViewModel){
  return this.http.post(`${environment.apiUrl}api/Account/register`, model);
}
login(model: LoginModel): Observable<JwtView> {
  return this.http.post<JwtView>(`${environment.apiUrl}api/Account/login`, model);
}
forgotPassword(model : ForgotPasswordView){
  return this.http.post(`${environment.apiUrl}api/Account/forgot`, model);
}
changeEmail(model:ChangeEmailView){
  return this.http.post(`${environment.apiUrl}api/Account/changeEmail`, model);  
}
changePassword(model:ChangePasswordView){
  return this.http.post(`${environment.apiUrl}api/Account/changePassword`, model);  
}

}
