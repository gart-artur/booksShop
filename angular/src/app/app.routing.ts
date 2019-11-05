import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './guards/auth.guard';
import { AuthorComponent } from './author/author.component';
import { BookComponent } from './book/book.component';
import { CartComponent } from './cart/cart.component';
import { PaymentComponent } from './payment/payment.component';
import { CustomFormComponent } from './custom-form/custom-form.component';
import { OrderComponent } from './order/order.component';
import { RegisterComponent } from './register/register.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';

const appRoutes: Routes = [
    { path: '', component: HomeComponent, canActivate: [AuthGuard] },
    { path: ' ', redirectTo: 'login' },
    { path: 'login', component: LoginComponent },
    { path: 'authors', component: AuthorComponent },
    { path: 'books', component: BookComponent },
    { path: 'cart',component:CartComponent},
    { path: 'payment',component:PaymentComponent},
    { path: 'order',component:OrderComponent},
    { path: 'register',component:RegisterComponent},
    { path: 'forgot',component:ForgotPasswordComponent}



];

export const routing = RouterModule.forRoot(appRoutes);