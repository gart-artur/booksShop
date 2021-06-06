import { Component, OnInit } from '@angular/core';
import { CartService } from '../services/cart.service';
import { ProductOrder, ProductOrderBook } from '../models/book';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  books: ProductOrderBook [] = [];
  total: number;

  constructor(private cartService: CartService) { }

  ngOnInit() {
    this.books = this.cartService.getItemsFromCart();
    this.calculateTotal();
  }

  changeQty(book: ProductOrderBook, qty: number) {
    book.qty += qty;
    this.calculateTotal();
  }

  remove(book: ProductOrderBook) {
    this.books = this.books.filter(x => x.id !== book.id);
    const order: ProductOrder = {
      books: this.books
    };
    this.cartService.addItemToCart(order);
    this.calculateTotal();
  }

  private calculateTotal(): void {
    let total = 0;
    this.books.forEach(book => total += book.price * book.qty);
    this.total = total;
  }
}
