import { Component, OnInit, NgZone } from '@angular/core';
import { CartService } from '../services/cart.service';
import { ProductOrder, Book, ProductOrderBook } from '../models/book';
import { FormBuilder } from '@angular/forms';
import { PaymentViewModel } from '../models/paymentViewModel';
import { UserPayViewModel } from '../models/user-pay-view-model';
import { Router } from '@angular/router';
import { JwtView } from '../models/JwtView';
import { NotificationService } from '../services/notification.service';
import { tap } from 'rxjs/operators';
import { StripeScriptTag } from 'stripe-angular';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent implements OnInit {

  bookNameArray = '';
  shippings: ProductOrderBook[] = [];
  total: number;

  userpPayModel = new UserPayViewModel();
  userInfo: JwtView;

  cardNumber: string;
  expiryMonth: string;
  expiryYear: string;
  cvc: string;

  disablePaymentButton = false;

  constructor(
    private notificationService: NotificationService,
    private cartService: CartService,
    private zone: NgZone,
    private router: Router) {
  }

  ngOnInit() {
    this.shippings = this.cartService.getItemsFromCart();
    this.total = this.cartService.getTotlaPrice();
  }

  onSubmin(): void {
    this.notificationService.showSuccess('Your order has been submitted');
    this.cartService.clearCart();
  }

  async getToken() {
    this.disablePaymentButton = true;
    (window as any).Stripe.card.createToken({
      number: this.cardNumber,
      exp_month: this.expiryMonth.substr(0, this.expiryMonth.indexOf('/')),
      exp_year: this.expiryMonth.substr(-2, this.expiryMonth.indexOf('/')),
      cvc: this.cvc
    }, (status: number, response: any) => {
      this.zone.run(async () => {
        if (status === 200) {
          this.userpPayModel.token = response.id;
          this.userpPayModel.total = this.total;
          this.userpPayModel.bookName = this.getNameOfAllBook();
          this.cartService.sendStripePayment(this.userpPayModel)
            .pipe(
              tap(() =>  {
                this.notificationService.showSuccess('The payment was successful' );
                this.disablePaymentButton = false;
                this.cartService.clearCart();
              })
            )
            .subscribe((() => this.router.navigateByUrl('/order')));
        } else {
          this.notificationService.showError(response.error.message);
          this.disablePaymentButton = false;
        }
      });

    });
  }


  private getNameOfAllBook() {
    this.bookNameArray = '';
    for (let i = 0; i < this.shippings.length; i++) {
      this.bookNameArray += [i + 1] + ')' + (this.shippings[i].name) + '.  ';
    }
    return this.bookNameArray;
  }

  async sendUserModel() {
    await this.getToken();
  }
}
