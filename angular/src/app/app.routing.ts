import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './guards/auth.guard';
import { AuthorComponent } from './author/author.component';
import { BookComponent } from './book/book.component';
import { CartComponent } from './cart/cart.component';
import { PaymentComponent } from './payment/payment.component';

const appRoutes: Routes = [
    { path: '', component: HomeComponent, canActivate: [AuthGuard] },
    { path: ' ', redirectTo: 'login' },
    { path: 'login', component: LoginComponent },
       // otherwise redirect to home
    { path: 'authors', component: AuthorComponent },
    { path: 'books', component: BookComponent },
    { path: 'cart',component:CartComponent},
    { path: 'payment',component:PaymentComponent}
    
];

export const routing = RouterModule.forRoot(appRoutes);