export class Book {
    id: number;
    price: number;
    name: string;
    dateCreated = Date.now();

    constructor() {
        this.dateCreated = Date.now()
    }
}

export class ProductOrder {
    product: Book[];
    quantity: number;
    totalPrice: number;
    constructor() {
        this.product = [];
    }
}

export class ProductOrders {
    productOrders: ProductOrder[] = [];
}

export class FilterBooksListView {
    books: Book[];
}