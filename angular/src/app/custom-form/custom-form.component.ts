import { Component, OnInit, NgZone } from '@angular/core';
import { PaymentViewModel } from '../models/paymentViewModel';
import { UserPayViewModel } from '../models/user-pay-view-model';
import { CartService } from '../services/cart.service';

@Component({
  moduleId : module.id,
  selector: 'app-custom-form',
  templateUrl: './custom-form.component.html',
  styleUrls: ['./custom-form.component.css']
})
export class CustomFormComponent  {
  userpPayModel  = new UserPayViewModel;
  cardNumber: string;
  expiryMonth: string;
  expiryYear: string;
  cvc: string;
  message : string; 

  constructor(private _zone: NgZone,
    private _cartService : CartService) {}    

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
          this.userpPayModel.total = 500;
          this._cartService.sendStripePayment(this.userpPayModel).subscribe();
        } else {
          this.message = response.error.message;
        }
      });

    });    
  }

  async sendUserModelWithToken(){
    await this.getToken()
    
  }
}  

