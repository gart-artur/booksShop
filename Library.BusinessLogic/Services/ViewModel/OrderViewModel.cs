using System;
using System.Collections.Generic;

namespace Library.BusinessLogic.Services.ViewModel
{
    public class OrderViewModel
    {
        public List<GetAllOrderViewItem> Orders { get; set; }
        public OrderViewModel()
        {
            Orders = new List<GetAllOrderViewItem>();
        }
    }

    public class GetAllOrderViewItem
    {
        public string Email { get; set; }
        public long Total { get; set; }
        public string BookName { get; set; }
        public string Status { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
