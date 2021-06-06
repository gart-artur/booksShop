export class Author {
    total: number;
    id: number;
    name: string;
    books: AuthorWithBooks[] = [];
}

export class AuthorWithBooks {
    id: string;
    name: string;
    price: string;
}
