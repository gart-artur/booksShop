import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Author } from '../models/author';
import { environment } from 'src/environments/environment';
import { FilterRequest } from '../models/filter-request';
import { Observable } from 'rxjs';

@Injectable()
export class AuthorService {
    constructor(private http: HttpClient) {
    }

    createAuthor(author: Author) {
        return this.http.post(`${environment.apiUrl}api/Authors/create`, author, { observe: 'response' });
    }

    deleteAuthor(id: number) {
        console.log(id);
        return this.http.post(`${environment.apiUrl}api/Authors/delete/${id}`, {});
    }
    getAuthors(request: FilterRequest): Observable<Author[]> {
        return this.http.post<Author[]>(`${environment.apiUrl}api/Authors/authors`, request);
    }
}
