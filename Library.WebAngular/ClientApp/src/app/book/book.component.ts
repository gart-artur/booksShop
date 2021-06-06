import { Component, OnInit } from '@angular/core';
import { Book, ProductOrder, ProductOrderBook } from '../models/book';
import { MatTableDataSource, PageEvent } from '@angular/material';
import { SelectionModel } from '@angular/cdk/collections';
import { BookService } from '../services/books.service';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';
import { CartService } from '../services/cart.service';
import { Observable, of } from 'rxjs';
import { NotificationService } from '../services/notification.service';
import { delay, tap } from 'rxjs/operators';
import { FilterRequest } from '../models/filter-request';

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.scss'],
  providers: [BookService]
})

export class BookComponent implements OnInit {
  displayedColumns: string[] = ['name', 'price', 'authors', 'text'];
  booksBySearch: Book[] = [];
  books: Book[] = [];
  total: number;

  pagination = new FilterRequest();
  pageSize = 10;

  productItem = new ProductOrder();

  constructor(
    private booksService: BookService,
    private cartService: CartService,
    private notificationService: NotificationService) {
   }

    ngOnInit() {
    this.loadBooksByFilters();
    this.productItem.books = this.cartService.getItemsFromCart();
  }

  async loadBooksByFilters() {
    this.booksService.sortByParams(this.pagination)
      .pipe(
        tap(
          response => {
            this.books = response;
            this.booksBySearch = this.books;
          }
        )
      ).subscribe(() => this.total = this.books.length > 0 ? this.books[0].total : 0);
  }

  search(event: KeyboardEvent) {
    const searchString = (event.target as HTMLInputElement).value.trim().toLowerCase();

    this.books = this.booksBySearch.filter(x =>
      x.name.toLowerCase().match(searchString) ||
      x.price.toString().match(searchString) ||
      x.authors.some(s => s.name.toLocaleLowerCase().includes(searchString)));
  }

  addBookToCart(book: Book): void {
    if (this.productItem.books.some(x => x.id === book.id)) {
      this.notificationService.showError('This book alreade added to your Shopping Bucket');
      setTimeout(() => {
        this.notificationService.showInfo('You can change a quantity of some product in your Shopping Bucket');
      }, 3000);
    } else {
      const order: ProductOrderBook = {
        id: book.id,
        name : book.name,
        price: book.price,
        qty : 1
      };
      this.productItem.books.push(order);
      this.cartService.addItemToCart(this.productItem);

      this.notificationService.showSuccess('\"' + order.name + '\"' + ' was successfully added');
    }
  }

  onPageChange(event: PageEvent): void {
    this.pagination.skip = event.pageIndex = event.pageIndex * event.pageSize;
    this.pagination.take = event.pageSize;

    this.loadBooksByFilters();
  }
}
