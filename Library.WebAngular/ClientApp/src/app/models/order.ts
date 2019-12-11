export class OrderViewModels {
    orders : Order[];
    constructor(){
        this.orders = [];
    }
}

export class Order {
    email: string;
    total: number;
    bookName: string;
    status: string;
    dateCreated: Date;
}