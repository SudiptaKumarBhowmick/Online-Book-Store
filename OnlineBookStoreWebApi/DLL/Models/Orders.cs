using DLL.Models.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DLL.Models
{
    public class Orders: ITrackable
    {
        [Key]
        public int OrderId { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime DateOrderPlaced { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string LocationOrderPlaced { get; set; }
        public Nullable<int> UserId { get; set; }
        public User User { get; set; }
        public Nullable<int> OrderStatusCode { get; set; }
        public RefOrderStatusCodes RefOrderStatusCodes { get; set; }
        public ICollection<OrderItems> OrderItems { get; set; }
        public Invoices Invoices { get; set; }
        public Shipment Shipment { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset LastUpdateAt { get; set; }
        public string LastUpdateBy { get; set; }
    }
}
