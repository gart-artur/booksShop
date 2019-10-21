import { Component, OnInit } from '@angular/core';
import { CartService } from '../services/cart.service';
import { ProductOrder } from '../models/book';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent implements OnInit {

  shippings :ProductOrder [] = [];
  checkForm;
  constructor( private _cartServce : CartService,
    private formBulder : FormBuilder
  ) {
    this.checkForm = this.formBulder.group({
      name: '',
      address : '',
      telephone : ''
    })
  }
    
  ngOnInit() {
    this.shippings = this._cartServce.getItemsFromCart().product;
  }
onSubmin(){
   window.alert('Your order has been submitted');
  this.checkForm.reset();
  return this._cartServce.clearCart();

}

}
