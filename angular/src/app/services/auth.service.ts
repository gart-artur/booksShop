import { Injectable } from '@angular/core';
import { JwtView } from '../models/JwtView';
import { Subject } from 'rxjs';
import { LocalStorage } from '@ngx-pwa/local-storage';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private readonly _userDataKey: string = "USER_DATA_KEY";
  private _currentUserData: JwtView;
  private _serviceMenuState: Subject<any> = new Subject<any>();
  observableMenuState = this._serviceMenuState.asObservable();

  public cookieChanged: Subject<JwtView> = new Subject<JwtView>();
  public logoutEvent: Subject<boolean> = new Subject<boolean>();
  public loginEvent: Subject<boolean> = new Subject<boolean>();

  constructor(private storage: LocalStorage,
    public _http: HttpClient,
    private router: Router) {
    this.storage.getItem<JwtView>(this._userDataKey).subscribe((data: JwtView) => {
      this._currentUserData = data;
    });
  }

  public login(data: JwtView): void {
    this._currentUserData = data;
    this.storage.setItemSubscribe(this._userDataKey, data);
    this.cookieChanged.next(this._currentUserData);
    this.loginEvent.next(true);
  }

  public logout(): void {
    this._currentUserData = null;
    this.storage.removeItem(this._userDataKey).subscribe(() => {
      this.logoutEvent.next(true);
      this.router.navigate(['auth', 'login']);
    });
  }

  public getAccessToken(): string {
    if (this._currentUserData) {
      return this._currentUserData.accessToken;
    }
    return undefined;
  }
  isAuthenticated(): Promise<boolean> {
    return new Promise((resolve) => {
      if (!this._currentUserData) {
        this.storage.getItem<JwtView>(this._userDataKey).subscribe((data) => {
          resolve(!!data);
        });
      }
      else {
        resolve(!!this._currentUserData);
      }
    });
  }

}
