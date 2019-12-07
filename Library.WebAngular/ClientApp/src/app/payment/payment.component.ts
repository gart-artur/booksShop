import { Component, OnInit, NgZone } from '@angular/core';
import { CartService } from '../services/cart.service';
import { ProductOrder, Book } from '../models/book';
import { FormBuilder } from '@angular/forms';
import { PaymentViewModel } from '../models/paymentViewModel';
import { UserPayViewModel } from '../models/user-pay-view-model';
import { Router } from '@angular/router';
import { JwtView } from '../models/JwtView';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent implements OnInit {

  bookNameArray: string = '';
  shippings: Book[] = [];
  shippingsItem: ProductOrder[] = [];
  priceForOrder: ProductOrder;
  // allname :ProductOrder [] = [];

  userpPayModel = new UserPayViewModel;
  userInfo: JwtView;

  checkForm;

  cardNumber: string;
  expiryMonth: string;
  expiryYear: string;
  cvc: string;
  message: string;

  constructor(private _cartService: CartService,
    private formBulder: FormBuilder,
    private _zone: NgZone,
    private router: Router) { }

  ngOnInit() {
    this.shippings = this._cartService.getItemsFromCart().product
    this.shippingsItem = this._cartService.getItemsFromCart().product.name;
    this.priceForOrder = this._cartService.getItemsFromCart().totalPrice
  }

  onSubmin() {
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

      this._zone.run(async () => {
        if (status === 200) {
          this.userpPayModel.token = response.id
          this.userpPayModel.total = this._cartService.getItemsFromCart().totalPrice
          this.userpPayModel.bookName = this.getNameOfAllBook();
          this._cartService.sendStripePayment(this.userpPayModel).subscribe((response=>this.router.navigateByUrl("/order")))
          this.bookNameArray = '';          
        } else {
          this.message = response.error.message;
        }
      });

    });
  }

  getNameOfAllBook() {
    for (let i = 0; i < this.shippings.length; i++) {
      this.bookNameArray += [i + 1] + ')' + (this.shippings[i].name) + '.  '
    }
    return this.bookNameArray
  }

  async sendUserModel() {
    await this.getToken()
  }
}
