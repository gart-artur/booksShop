import { Component, OnInit } from '@angular/core';
import { Author } from '../models/author';
import { MatTableDataSource } from '@angular/material';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';
import { AuthorService } from '../services/authors.service';
import { SelectionModel } from '@angular/cdk/collections';

let ELEMENT_DATA: Author[] = [];
@Component({
  selector: 'app-author',
  templateUrl: './author.component.html',
  styleUrls: ['./author.component.scss'],
  providers: [AuthorService]

})

export class AuthorComponent implements OnInit {
  displayedColumns: string[] = ['select', 'id', 'name', 'text'];
  dataSource = new MatTableDataSource<Author>(ELEMENT_DATA);
  selection = new SelectionModel<Author>(true, []);
  author: Author;
  editForm: FormGroup
  tableMode: boolean = true;
  openPopup: boolean;
  textButton: string;

  constructor(
    private formBuilder: FormBuilder,
    private authorServise: AuthorService,
  ) {
  this.editForm = new FormGroup({
    name: new FormControl('')
  })

  }

  ngOnInit() {

    this.loadAuthors();
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

  checkboxLabel(row?: Author): string {
    if (!row) {
      return `${this.isAllSelected() ? 'select' : 'deselect'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.id + 1}`;
  }

  openAddAuthorPopup(text) {
    this.openPopup = true
    this.textButton = text;
  }

  async loadAuthors() {
    ELEMENT_DATA = [];
    this.authorServise.getAuthors()
      .subscribe((authors: any) => {
        authors.authors.forEach((element: Author) => {
          ELEMENT_DATA.push(element);
          this.dataSource = new MatTableDataSource<Author>(ELEMENT_DATA);

        })
        console.log(ELEMENT_DATA)

      });
  }

  deleteAuthor(author: Author) {
    this.authorServise.deleteAuthor(author.id).subscribe(data => console.log(data)
    );
    console.log('delete')
  }

  add(a: Author) {
    this.authorServise.createAuthor(this.editForm.value)
      .subscribe(data => console.log(data)
      );
    ELEMENT_DATA.push(this.editForm.value);
    this.dataSource = new MatTableDataSource<Author>(ELEMENT_DATA);
    console.log(a)
  }

  cancel() {
    this.author = new Author();
    this.tableMode = true;
  }

  add1() {
    this.cancel();
    this.tableMode = false;
  }
}