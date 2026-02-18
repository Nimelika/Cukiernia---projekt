using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MakeAWishDB.Entities
{
    [Index("OrderDate", Name = "index_orders_orderdate")]
    [Index("Status", Name = "index_orders_status")]
    public partial class Order
    {
        [Key]
        [Column("OrderID")]
        public int OrderId { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? OrderDate { get; set; }

        public int? Customer { get; set; }

        [StringLength(100)]
        public string? GuestName { get; set; }

        [StringLength(100)]
        public string? GuestEmail { get; set; }

        [StringLength(20)]
        public string? GuestPhone { get; set; }

        public int Shop { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? CollectionDate { get; set; }

        public bool? IsPaid { get; set; }

        public int Status { get; set; }

        public bool IsActive { get; set; }

       
        [ForeignKey("Customer")]
        [InverseProperty("Orders")]
        public virtual Customer? CustomerNavigation { get; set; }

        [InverseProperty("Order")]
        public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

        [InverseProperty("Order")]
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        [ForeignKey("Shop")]
        [InverseProperty("Orders")]
        public virtual Shop? ShopNavigation { get; set; }

        [ForeignKey("Status")]
        [InverseProperty("Orders")]
        public virtual Status? StatusNavigation { get; set; }
    }
}
