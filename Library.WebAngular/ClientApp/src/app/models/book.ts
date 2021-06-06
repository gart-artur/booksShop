export class Book {
    total: number;
    id: number;
    price: number;
    name: string;
    authors: AuthorResponse [] = [];
}

export class AuthorResponse {
    id: number;
    name: string;
}

export class ProductOrder {
    books: ProductOrderBook[] = [];

    constructor() {
    }
}

export class ProductOrderBook {
    id: number;
    name: string;
    price: number;
    qty: number;
}

export class ProductOrders {
    productOrders: ProductOrder[] = [];
}

export class FilterBooksListView {
    books: Book[];
}
