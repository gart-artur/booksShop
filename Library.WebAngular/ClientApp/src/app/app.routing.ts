import { Routes, RouterModule } from '@angular/router';

import { LoginComponent } from './login/login.component';
import { AuthorComponent } from './author/author.component';
import { BookComponent } from './book/book.component';
import { CartComponent } from './cart/cart.component';
import { PaymentComponent } from './payment/payment.component';
import { OrderComponent } from './order/order.component';
import { RegisterComponent } from './register/register.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { GoAuthGuard } from './guards/goAuth.guard';
import { AuthGuard } from './guards/auth.guard';
import { CahngeEmailComponent } from './cahnge-email/cahnge-email.component';
import { ChangePasswordComponent } from './change-password/change-password.component';



const appRoutes: Routes = [    
    { path: ' ',redirectTo:'login'},
    { path: 'login', component: LoginComponent},
    { path: 'logout', component: LoginComponent},
    { path: 'register',component:RegisterComponent},
    { path: 'forgot',component:ForgotPasswordComponent},
    
    { path: 'authors', component: AuthorComponent,canActivate: [AuthGuard] },
    { path: 'books', component: BookComponent , canActivate: [AuthGuard]},
    { path: 'cart',component:CartComponent , canActivate: [AuthGuard]},
    { path: 'payment',component:PaymentComponent , canActivate: [AuthGuard]},
    { path: 'order',component:OrderComponent , canActivate: [AuthGuard]},
    { path: 'changeEmail',component:CahngeEmailComponent , canActivate: [AuthGuard]},
    { path: 'changePassword',component:ChangePasswordComponent , canActivate: [AuthGuard]},

    { path: '**',redirectTo:'login'}
    

];

export const routing = RouterModule.forRoot(appRoutes);