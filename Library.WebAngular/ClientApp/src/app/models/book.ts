export class Book {
    id: number;
    price: number;
    name: string;
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