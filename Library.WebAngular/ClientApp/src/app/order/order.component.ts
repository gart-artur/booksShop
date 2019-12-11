import { Component, OnInit } from '@angular/core';
import { Order, OrderViewModels } from '../models/order';
import { OrderService } from '../services/order.service';
import { Observable } from 'rxjs';
import { element } from 'protractor';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {

  searchModel: OrderViewModels;
  dataModel : Order [] = [];

  constructor(private _orderService: OrderService) { }

  ngOnInit() {
    this.loadOrders()
  }  

  loadOrders(): void {
    this._orderService.getAllOrders().subscribe(response => {
      this.initialDataAfterSearch(response);
    })
  }

  initialDataAfterSearch(data: OrderViewModels) {
    this.searchModel = data;
    this.search();
  }

  search() : void{
    this.searchModel.orders.forEach((el:Order)=>{
      console.log(el)
      this.dataModel.push(el)
    })
  }

}

