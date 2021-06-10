import { Component, OnInit } from '@angular/core';
import { Order, OrderViewModels } from '../models/order';
import { OrderService } from '../services/order.service';
import { Observable } from 'rxjs';
import { element } from 'protractor';
import { MatDialog } from '@angular/material';
import { OrderDialogComponent } from '../order-dialog/order-dialog.component';
import { LoadService } from '../services/load.service';
import { finalize, tap } from 'rxjs/operators';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {

  searchModel: OrderViewModels;
  dataModel: Order[] = [];

  constructor(
    private orderService: OrderService,
    private dialog: MatDialog) { }

  ngOnInit() {
    this.loadOrders();
  }

  loadOrders(): void {
    this.orderService.getAllOrders()
    .subscribe(response => this.initialDataAfterSearch(response));
  }

  initialDataAfterSearch(data: OrderViewModels) {
    this.searchModel = data;
    this.search();
  }

  search(): void {
    this.searchModel.orders.forEach((el: Order) => {
      console.log(el);
      this.dataModel.push(el);
    });
  }

  shipping(order: Order) {
    this.dialog.open(OrderDialogComponent, {
      width: '850px',
      data: {order}
    });
  }

}

