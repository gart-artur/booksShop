import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule, FormsModule }    from '@angular/forms';
import { MatTableModule } from '@angular/material'  


// used to create fake backend

import { AppComponent }  from './app.component';
import { routing }        from './app.routing';
import { HomeComponent } from './home/home.component';
import { AuthorComponent } from './author/author.component';
import { BookComponent } from './book/book.component';
import { BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { MatNativeDateModule} from '@angular/material/core';
import { MatCheckboxModule} from '@angular/material/checkbox';
import { MatButtonModule} from '@angular/material/button';
import { MatListModule} from '@angular/material/list';
import { DemoMaterialModule} from './material-module';
import { MatSidenavModule} from '@angular/material';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { CartComponent } from './cart/cart.component';
import { PaymentComponent } from './payment/payment.component';
import { NgxStripeModule } from 'ngx-stripe';
import { CustomFormComponent } from './custom-form/custom-form.component';
import { OrderComponent } from './order/order.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { HeaderComponent } from './header/header.component';
import { AuthInterceptor } from './Interceptor/auth-interceptor';
import { ToastContainerModule, ToastrModule } from 'ngx-toastr';
import { AuthGuard } from './guards/auth.guard';
import { GoAuthGuard } from './guards/goAuth.guard';
import { CahngeEmailComponent } from './cahnge-email/cahnge-email.component';
import { ChangePasswordComponent } from './change-password/change-password.component';











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
      MatSidenavModule,
      ToastContainerModule,
      ToastrModule.forRoot()//StorageModule.forRoot(IDBNoWrap)
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
      OrderComponent,
      RegisterComponent,
      ForgotPasswordComponent,
      HeaderComponent,
      CahngeEmailComponent,
      ChangePasswordComponent
   ],
   providers: [
      AuthGuard,
      { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
   ],
   bootstrap: [
      AppComponent
   ]
})


export class AppModule { }