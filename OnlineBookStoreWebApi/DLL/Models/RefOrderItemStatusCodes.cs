using DLL.Models.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace DLL.Models
{
    public class RefOrderItemStatusCodes: ITrackable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderItemStatusCode { get; set; }
        [Column(TypeName = "varchar(15)")]
        public string OrderItemStatusDescription { get; set; } //Delivered, Out of stock
        public ICollection<OrderItems> OrderItems { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset LastUpdateAt { get; set; }
        public string LastUpdateBy { get; set; }
    }
}
