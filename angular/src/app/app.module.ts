import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule, FormsModule }    from '@angular/forms';
import { MatTableModule } from '@angular/material'  


// used to create fake backend
import { fakeBackendProvider } from './helpers/fake-backend';

import { AppComponent }  from './app.component';
import { routing }        from './app.routing';
import { BasicAuthInterceptor } from './helpers/basic-auth.interceptor';
import { ErrorInterceptor } from './helpers/error.interceptor';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { AuthorComponent } from './author/author.component';
import { BookComponent } from './book/book.component';
import { BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { MatNativeDateModule, MatRippleModule} from '@angular/material/core';
import { MatCheckboxModule} from '@angular/material/checkbox';
import { MatButtonModule} from '@angular/material/button';
import { MatListModule} from '@angular/material/list';
import { DemoMaterialModule} from './material-module';
import { MatSidenavModule} from '@angular/material';
import { platformBrowserDynamic} from '@angular/platform-browser-dynamic';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { CartComponent } from './cart/cart.component';
import { PaymentComponent } from './payment/payment.component';
import { NgxStripeModule } from 'ngx-stripe';
import { CustomFormComponent } from './custom-form/custom-form.component';
import { OrderComponent } from './order/order.component';









@NgModule({
   imports: [
      NgxStripeModule.forRoot('pk_test_UYSu39pyn95AcTe5TEGgCcsF00wyTDSaR9'),
      BrowserModule,
      ReactiveFormsModule,
      HttpClientModule,
      routing,
      MatTableModule,
      FormsModule,
      BrowserAnimationsModule,
      FormsModule,
      MatCheckboxModule,
      DemoMaterialModule,
      MatNativeDateModule,
      MatButtonModule,
      MatListModule,
      DemoMaterialModule,
      MatSidenavModule
   ],
   declarations: [
      AppComponent,
      HomeComponent,
      LoginComponent,
      AuthorComponent,
      BookComponent,
      CartComponent,
      PaymentComponent,
      CustomFormComponent,
      OrderComponent
   ],
   providers: [],
   bootstrap: [
      AppComponent
   ]
})


export class AppModule { }