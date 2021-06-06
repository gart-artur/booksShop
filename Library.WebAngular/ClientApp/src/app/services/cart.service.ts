import { Injectable } from '@angular/core';
import { ProductOrder, ProductOrderBook } from '../models/book';
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
    this.clearCart();
    localStorage.setItem('products', JSON.stringify(product.books));
  }

  getItemsFromCart(): ProductOrderBook[] {
    const products: ProductOrderBook[] = JSON.parse(localStorage.getItem('products'));
    return products ? products : [] ;
  }

  sendStripePayment(model: UserPayViewModel) {
    return this.http.post(`${environment.apiUrl}api/Stripe/charge`, model, { observe: 'response' });
  }

  getTotlaPrice(): number {
    let total = 0;
    const books: ProductOrderBook[] = this.getItemsFromCart();
    books.forEach(book => total += book.price * book.qty);
    return total;
  }

  clearCart() {
    localStorage.clear();
  }

}
