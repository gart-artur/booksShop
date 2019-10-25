import { Component, OnInit, NgZone } from '@angular/core';
import { CartService } from '../services/cart.service';
import { ProductOrder } from '../models/book';
import { FormBuilder } from '@angular/forms';
import { PaymentViewModel } from '../models/paymentViewModel';
import { UserPayViewModel } from '../models/user-pay-view-model';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent implements OnInit {

  bookNameArray:string='';
  shippings :ProductOrder [] = [];
  priceForOrder : ProductOrder;
  allname :ProductOrder [] = [];

  userpPayModel = new UserPayViewModel;
  
  checkForm;
  
  cardNumber: string;
  expiryMonth: string;
  expiryYear: string;
  cvc: string;
  message : string; 

  constructor( private _cartService : CartService,
    private formBulder : FormBuilder,
    private _zone : NgZone) { }
    
  ngOnInit() {
    this.shippings = this._cartService.getItemsFromCart().product
    this.priceForOrder = this._cartService.getItemsFromCart().totalPrice
  }  
  
  onSubmin(){
    window.alert('Your order has been submitted');
    this.checkForm.reset();
    return this._cartService.clearCart();
  }
  async getToken() {
    this.message = 'Loading...';

    (<any>window).Stripe.card.createToken({
      number: this.cardNumber,
      exp_month: this.expiryMonth,
      exp_year: this.expiryYear,
      cvc: this.cvc
    }, (status: number, response: any) => {
      
      this._zone.run(() => {
        if (status === 200) {          
          this.message = `Success! Card token ${response.id}.`
          this.userpPayModel.token = response.id
          this.userpPayModel.email = "artur.hart@nure.ua"
          this.userpPayModel.total =  this._cartService.getItemsFromCart().totalPrice
          this.userpPayModel.bookName = this.getNameOfAllBook();       
          this._cartService.sendStripePayment(this.userpPayModel).subscribe();
          this.bookNameArray='';

        } else {
          this.message = response.error.message;
        }
      });

    });    
  }

      // getNameOfAllBook (){
      //   for(let i=0; i<this.shippings.length;i++){
      //     this.bookNameArray+=(this.shippings[i].name)+','
      //   }    
      //   return this.bookNameArray
      // }
     
getNameOfAllBook (){
  for(let i=0; i<this.shippings.length;i++){
    for (let j=0; j<this.shippings.length;j++){
      this.bookNameArray+=(this.shippings[i].product[j].name)+''}

  }  
  return this.bookNameArray;
 }

  async sendUserModel(){
    await this.getToken()    
  }


}
