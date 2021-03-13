using DLL.Models.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DLL.Models
{
    public class Product: ITrackable
    {
        [Key]
        public int ProductId { get; set; }
        [Column(TypeName = "varchar(8)")]
        public string ProductCode { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string ProductName { get; set; }
        [Column(TypeName ="decimal(8,3)")]
        public decimal ProductPrice { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string ProductImage { get; set; }
        public int ProductCategroyId { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public ProductDescription ProductDescriptions { get; set; }
        public ProductReview ProductReview { get; set; }
        public ProductInStock ProductInStock { get; set; }
        public ICollection<OrderItems> OrderItems { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset LastUpdateAt { get; set; }
        public string LastUpdateBy { get; set; }
    }
}
