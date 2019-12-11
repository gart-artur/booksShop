import { Component, OnInit, ViewChild } from '@angular/core';
import { CartService } from '../services/cart.service';
import { ProductOrder } from '../models/book';
import { MatSidenav } from '@angular/material';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  items:ProductOrder[]=[];
  sumOfPrice:ProductOrder;

  constructor(private _cartService : CartService) { }

  ngOnInit() {    
    this.items = this._cartService.getItemsFromCart().product
    this.sumOfPrice = this._cartService.getItemsFromCart().totalPrice
  }
 
  
  

  

  



}
