import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './guards/auth.guard';
import { AuthorComponent } from './author/author.component';
import { BookComponent } from './book/book.component';

const appRoutes: Routes = [
    { path: '', component: HomeComponent, canActivate: [AuthGuard] },
    { path: 'login', component: LoginComponent },
       // otherwise redirect to home
    { path: ' ', redirectTo: 'login' },
    { path: 'authors', component: AuthorComponent },
    { path: 'books', component: BookComponent }
];

export const routing = RouterModule.forRoot(appRoutes);