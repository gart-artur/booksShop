import { Component, OnInit } from '@angular/core';
import { Book, ProductOrder } from '../models/book';
import { MatTableDataSource } from '@angular/material';
import { SelectionModel } from '@angular/cdk/collections';
import { BookService } from '../services/books.service';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';
import { CartService } from '../services/cart.service';
import { Observable, of } from 'rxjs';
import { SortBooksByParamsView } from '../models/sort-books-by-params-view';

let BOOKS_ARRAY: Book[] = [];

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.scss'],
  providers: [BookService]
})

export class BookComponent implements OnInit {

  displayedColumns: string[] = ['select', 'id', 'name', 'price', 'text'];
  dataSource = new MatTableDataSource<Book>(BOOKS_ARRAY);
  selection = new SelectionModel<Book>(true, []);
  book: Book;
  tableMode: boolean = true;
  editForm: FormGroup;
  filterForm: FormGroup;
  openPopup: boolean;
  textButton: string;

  productItem = new ProductOrder();

  constructor(
    private _booksService: BookService,
    private _cartService: CartService,
    private _formBuilder: FormBuilder) {
    this.editForm = new FormGroup({
      name: new FormControl(''),
      price: new FormControl(''),
      dateCreated: new FormControl('')
    })
    this.productItem.totalPrice = 0;
  }

  ngOnInit() {
    this.filterForm = this._formBuilder.group({
      name: [''],
      minPrice: [''],
      maxPrice: ['']
    });
    this.loadBooksByFilters();
    const currentDate = new Date().toISOString().substring(0, 10);
    this.editForm.controls['dateCreated'].setValue(currentDate);
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }

  masterToggle() {
    this.isAllSelected() ?
      this.selection.clear() :
      this.dataSource.data.forEach(row => this.selection.select(row));
  }

  checkboxLabel(row?: Book): string {
    if (!row) {
      return `${this.isAllSelected() ? 'select' : 'deselect'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.id + 1}`;
  }

  openAddBookPopup(text) {
    this.openPopup = true
    this.textButton = text;
  }

  sortItem() {
    let sortparams = new SortBooksByParamsView();
    sortparams.name = this.filterForm.controls['name'].value == null ? '' : this.filterForm.controls['name'].value
    sortparams.minPrice = this.filterForm.controls['minPrice'].value == 0 ? 0 : this.filterForm.controls['minPrice'].value
    sortparams.maxPrice = this.filterForm.controls['maxPrice'].value == 0 ? 0 : this.filterForm.controls['maxPrice'].value
    return sortparams;
  }

  async loadBooksByFilters() {
    BOOKS_ARRAY = [];
    let getNullFilters = await this.sortItem()
    this._booksService.sortByParams(getNullFilters)
      .subscribe((item: any) => {
          item.books.forEach((element: Book) => {
            BOOKS_ARRAY.push(element);
            this.dataSource = new MatTableDataSource<Book>(BOOKS_ARRAY);
          })
      });
  }

  deleteBook(book: Book) {
    this._booksService.deleteBook(book.id).subscribe();
    return this.loadBooksByFilters();
  }

  add(a: Book) {
    this._booksService.createBook(this.editForm.value)
      .subscribe(data => console.log(data)
      );
    BOOKS_ARRAY.push(this.editForm.value);
    this.dataSource = new MatTableDataSource<Book>(BOOKS_ARRAY);
  }

  cancel() {
    this.book = new Book();
    this.tableMode = true;
  }

  addBookToCart(item: Book) {
    window.alert("This book success add to Shoping Cart");
    this.productItem.product.push(item)
    this.productItem.quantity = 1;
    this.productItem.totalPrice += item.price;
    this._cartService.addItemToCart(this.productItem);
  }
}