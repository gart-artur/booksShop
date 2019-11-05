using System;

namespace Library.DataAccess.Entities
{
    public class Order : BaseEntity
    {        
        public string BalanceTransactionId { get; set; }
        public string Email { get; set; }
        public long Total { get; set; }
        public string BookName { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }        
    }
}
