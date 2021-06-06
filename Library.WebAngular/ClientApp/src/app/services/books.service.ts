import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Book } from '../models/book';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { FilterRequest } from '../models/filter-request';

@Injectable()
export class BookService {
    constructor(private http: HttpClient) {
    }

    createBook(book: Book) {
        return this.http.post(`${environment.apiUrl}api/Books/create`, book, { observe: 'response' });
    }
    deleteBook(id: number) {
        return this.http.post(`${environment.apiUrl}api/Books/delete/${id}`, {});
    }
    getBooks() {
        return this.http.get(`${environment.apiUrl}api/Books/books`);
    }
    editBooks(book: Book) {
        return this.http.post(`${environment.apiUrl}api/Books/edit`, book, { observe: 'response' });
    }
    sortByParams(model: FilterRequest): Observable<Book[]> {
        return this.http.post<Book[]>(`${environment.apiUrl}api/Books/sort`, model);
    }
}