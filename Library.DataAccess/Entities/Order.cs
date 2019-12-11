using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.DataAccess.Entities
{
    [Table("Orders")]
    public class Order : BaseEntity
    {
        [Column("BalanceTransactionId")]
        public string BalanceTransactionId { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("Total")]
        public long Total { get; set; }

        [Column("BookName")]
        public string BookName { get; set; }

        [Column("Status")]
        public string Status { get; set; }

        [Column("Created")]
        public DateTime Created { get; set; } = DateTime.Now;

        [ForeignKey("UserId")]
        public User User { get; set; }

        [Column("UserId")]
        public string UserId { get; set; }
    }
}
