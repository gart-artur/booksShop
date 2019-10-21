import { Component, OnInit } from '@angular/core';
import { Book, ProductOrder } from '../models/book';
import { MatTableDataSource } from '@angular/material';
import { SelectionModel } from '@angular/cdk/collections';
import { BookService } from '../services/books.service';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';
import { CartService } from '../services/cart.service';



let ELEMENT_DATA : Book[] = [];


@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.scss'],
  providers: [BookService]
  
})

export class BookComponent implements OnInit {

  displayedColumns: string[] = ['select', 'id','name','price','text'];
  // displayedColumns1: string[] = ['Book', 'Price'];
  dataSource = new MatTableDataSource<Book>(ELEMENT_DATA); 
  selection = new SelectionModel<Book>(true, []);
  book:Book;
  tableMode: boolean = true;
  editForm: FormGroup;
  openPopup: boolean;
  textButton: string;

  productItem = new ProductOrder();
  //  products : ProductOrder[] = [];

  constructor(
    private _booksService : BookService,
    private _cartService : CartService) 
    { this.editForm = new FormGroup({
      name: new FormControl(''),
      price : new FormControl(''),
      dataCreate : new FormControl ('')
  })}

  ngOnInit(){  
     this.loadBooks();  
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

  checkboxLabel(row?:Book): string {
    if (!row) {
      return `${this.isAllSelected() ? 'select' : 'deselect'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.id + 1}`;
  }

  openAddAuthorPopup(text) {
    this.openPopup = true
    this.textButton = text;
  }

  async loadBooks(){
    ELEMENT_DATA=[];
    this._booksService.getBooks()
    .subscribe((books :any) =>{
      books.books.forEach((element:Book)=>{
        ELEMENT_DATA.push(element);
        this.dataSource = new MatTableDataSource<Book>(ELEMENT_DATA);        
      }) 
      });
  }

  deleteBook(book : Book) {
    this._booksService.deleteBook(book.id).subscribe();   
    return this.loadBooks(); 
  }
    
  add( a  : Book){
    this._booksService.createBook(this.editForm.value)
       .subscribe(data => console.log(data)
       );        
       ELEMENT_DATA.push(this.editForm.value);
       this.dataSource = new MatTableDataSource<Book>(ELEMENT_DATA);
       console.log(a)
 }
 
  cancel(){
      this.book = new Book();
      this.tableMode = true;
  }

//  edit(b:Book){  
//    this._booksService.editBooks(b).subscribe(res  => this.dataSource );
//  }

addBookToCart(item:Book){
  window.alert("This book succes add to Shoping Cart"); 
  this.productItem.product.push(item)
  this.productItem.quantity=1;
  this._cartService.addItemToCart(this.productItem);
}


getTotalCost() {
  this.productItem.totalPrice=+this.productItem.product.map(t => t.price).reduce((acc, value) => acc + value, 0);
  return this.productItem.totalPrice;
}

}