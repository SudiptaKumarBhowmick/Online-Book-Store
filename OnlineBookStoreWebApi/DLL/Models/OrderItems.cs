using DLL.Models.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DLL.Models
{
    public class OrderItems: ITrackable
    {
        [Key]
        public int OrderItemId { get; set; }
        public int OrderItemQnt { get; set; }
        [Column(TypeName = "decimal(8,3)")]
        public decimal OrderItemPrice { get; set; }
        [Column(TypeName = "decimal(8,3)")]
        public decimal TotalAmount { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Product Products { get; set; }
        public Nullable<int> OrderId { get; set; }
        public Orders Orders { get; set; }
        public Nullable<int> OrderItemStatusCode { get; set; }
        public RefOrderItemStatusCodes RefOrderItemStatusCodes { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset LastUpdateAt { get; set; }
        public string LastUpdateBy { get; set; }
    }
}
