import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Author } from '../models/author';
import { environment } from 'src/environments/environment';

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
    getAuthors() {
        return this.http.get(`${environment.apiUrl}api/Authors/authors`);

    }
    // update(product: Author) {
    //     return this.http.put(`${environment.apiUrl}` + '/' + product.id, product);
    // }
}