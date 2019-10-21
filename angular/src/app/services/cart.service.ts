import { Injectable } from '@angular/core';
import { ProductOrder, ProductOrders } from '../models/book';

@Injectable({
  providedIn: 'root'
})
export class CartService {

constructor() { 
 }


addItemToCart(product:ProductOrder){
  localStorage.setItem('products' , JSON.stringify(product));  
}

getItemsFromCart(){
  return JSON.parse(localStorage.getItem('products'))
}

clearCart(){
  localStorage.clear();
}


}
