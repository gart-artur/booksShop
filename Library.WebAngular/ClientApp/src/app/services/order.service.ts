import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { OrderViewModels } from '../models/order';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor(private http: HttpClient) { }

  getAllOrders() : Observable<OrderViewModels>  {
    return this.http.get<OrderViewModels>(`${environment.apiUrl}api/Stripe/order`);
  }

}
