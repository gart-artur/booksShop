import { Component, OnInit, NgZone } from '@angular/core';
import { PaymentViewModel } from '../models/paymentViewModel';

@Component({
  moduleId : module.id,
  selector: 'app-custom-form',
  templateUrl: './custom-form.component.html',
  styleUrls: ['./custom-form.component.css']
})
export class CustomFormComponent implements OnInit {
  cartInfo = new PaymentViewModel;
  message : string; 

  constructor(private _zone : NgZone) { }

  getToken(){
    this.message = "Loading...";
    (<any>window).Stripe.card.createToken({
      number: this.cartInfo.cardNumber,
      exp_month: this.cartInfo.expiryMonth,
      exp_year: this.cartInfo.expiryYear,
      cvc: this.cartInfo.cvc
    }, (status: number, response: any) => {

      // Wrapping inside the Angular zone
      this._zone.run(() => {
        if (status === 200) {
          this.message = `Success! Card token ${response.card.id}.`;
        } else {
          this.message = response.error.message;
        }
      });
    });
  }
  }

  ngOnInit() {
  }

}
