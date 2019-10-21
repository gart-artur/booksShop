using Library.DataAccess.Entities;
using Library.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DataAccess.Repository
{
    public class ShoppCardRepository : IShoppCardRepository
    {
        protected MagazineContext _context;
        protected DbSet<ShoppingCart> _entities;
        public ShoppCardRepository(MagazineContext context)
        {
            _context = context;
            _entities = context.Set<ShoppingCart>();
        }
        public void AddToShoppingCart(ShoppingCart shoppingCart)
        {
            _entities.Add(shoppingCart);
            _context.SaveChanges();
        }

        public void DeleteItemFromCart(ShoppingCart shoppingCart)
        {
            _entities.Remove(shoppingCart);
            _context.SaveChanges();
        }

        public IEnumerable<ShoppingCart> GetAll()
        {
            return _entities.ToList();
        }
    }
}
