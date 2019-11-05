import { Component, OnInit } from '@angular/core';
import { Order, orderViewModels } from '../models/order';
import { OrderService } from '../services/order.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {

  orders: Observable<orderViewModels[]>;
  
  constructor(private _orderService:OrderService) {}

  ngOnInit() {
    this.loadOrders()
  }

  loadOrders(){
    this.orders =this._orderService.getAllOrders()
  }

}
