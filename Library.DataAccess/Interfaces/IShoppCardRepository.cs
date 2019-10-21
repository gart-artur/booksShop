using Library.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DataAccess.Interfaces
{
   public interface IShoppCardRepository
    {
        IEnumerable<ShoppingCart> GetAll();
        void AddToShoppingCart(ShoppingCart shoppingCart);
        void DeleteItemFromCart(ShoppingCart shoppingCart);
    }
}
