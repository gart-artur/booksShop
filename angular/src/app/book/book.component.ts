import { Component, OnInit } from '@angular/core';
import { Book } from '../models/book';
import { MatTableDataSource } from '@angular/material';
import { SelectionModel } from '@angular/cdk/collections';
import { BookService } from '../services/books.service';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';



let ELEMENT_DATA : Book[] = [];


@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.scss'],
  providers: [BookService]
  
})

export class BookComponent implements OnInit {

  displayedColumns: string[] = ['select', 'id','name','price','dataCreate','text'];
  dataSource = new MatTableDataSource<Book>(ELEMENT_DATA);
  selection = new SelectionModel<Book>(true, []);
  book:Book;
  tableMode: boolean = true;
  editForm: FormGroup;
  openPopup: boolean;
  textButton: string;


  constructor(
    private formBuilder : FormBuilder,
    private booksService : BookService)  {   this.editForm = new FormGroup({
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
    this.booksService.getBooks()
    .subscribe((books :any) =>{
      books.books.forEach((element:Book)=>{
        ELEMENT_DATA.push(element);
        this.dataSource = new MatTableDataSource<Book>(ELEMENT_DATA);
        
      })
      console.log(ELEMENT_DATA)
      
    });
  }

  deleteBook(author : Book) {
    this.booksService.deleteBook(author.id).subscribe(data => console.log(data)
    );   
    console.log('delete')  
  }
    
  add( a  : Book){
    this.booksService.createBook(this.editForm.value)
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

    add1() {
      this.cancel();
      this.tableMode = false;
  }
}