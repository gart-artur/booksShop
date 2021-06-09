import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})

export class LoadService {
  private appComponent: any;

  get isLoading(): boolean {
    return this.appComponent.showLoader;
  }

  set = (flag: boolean) => {
    this.appComponent.showLoader = flag;
  }

  passlinkObject = (obj: any) => {
    this.appComponent = obj;
  }
}
