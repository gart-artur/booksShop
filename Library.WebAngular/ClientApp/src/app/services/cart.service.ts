import { Injectable } from '@angular/core';
import { ProductOrder } from '../models/book';
import { UserPayViewModel } from '../models/user-pay-view-model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';



@Injectable({
  providedIn: 'root'
})
export class CartService {
  constructor(private http: HttpClient) {
  }

  addItemToCart(product: ProductOrder) {
    localStorage.setItem('products', JSON.stringify(product));
  }
  getItemsFromCart() {
    return JSON.parse(localStorage.getItem('products'))
  }
  sendStripePayment(model: UserPayViewModel) {
    return this.http.post(`${environment.apiUrl}api/Stripe/charge`, model, { observe: 'response' });
  }
  getTotlaPrice() {
    let myObject = JSON.parse(localStorage.getItem('products'));
    return myObject.totalPrice;
  }
  clearCart() {
    localStorage.clear();
  }

}
