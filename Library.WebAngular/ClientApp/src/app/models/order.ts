export class orderViewModels {
    orderViewModels: Array<Order>[];
}

export class Order {
    email: string;
    total: number;
    bookName: string;
    status: string;
    dateCreated: Date;
}