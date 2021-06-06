import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { tap } from 'rxjs/operators';
import { Author } from '../models/author';
import { FilterRequest } from '../models/filter-request';
import { AuthorService } from '../services/authors.service';

@Component({
  selector: 'app-author',
  templateUrl: './author.component.html',
  styleUrls: ['./author.component.scss'],
  providers: [AuthorService]
})

export class AuthorComponent implements OnInit {
  displayedColumns: string[] = ['name', 'books', 'details'];

  authorsBySearch: Author[] = [];
  authors: Author[] = [];
  total: number;

  pagination = new FilterRequest();
  pageSize = 10;

  constructor(
    private authorServise: AuthorService,
  ) {
  }

  ngOnInit() {
    this.loadAuthors();
  }

  async loadAuthors() {
    this.authorServise.getAuthors(this.pagination)
    .pipe(
      tap(
        response => {
          this.authors = response;
          this.authorsBySearch = this.authors;
        }
      )
    ).subscribe(() => this.total = this.authors.length > 0 ? this.authors[0].total : 0);
  }

  search(event: KeyboardEvent) {
    const searchString = (event.target as HTMLInputElement).value.trim().toLowerCase();

    this.authors = this.authorsBySearch.filter(x =>
      x.name.toLowerCase().match(searchString) ||
      x.books.some(s => s.name.toLocaleLowerCase().includes(searchString)));
  }

  onPageChange(event: PageEvent): void {
    this.pagination.skip = event.pageIndex = event.pageIndex * event.pageSize;
    this.pagination.take = event.pageSize;

    this.loadAuthors();
  }

  showDetails(author: Author): void {
  }
}
