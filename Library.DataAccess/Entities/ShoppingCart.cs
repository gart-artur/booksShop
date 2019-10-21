using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DataAccess.Entities
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public decimal PriceForOne { get; set; }
        public int CountItem { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
