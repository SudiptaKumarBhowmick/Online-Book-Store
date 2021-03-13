using DLL.Models.Interface;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DLL.Models
{
    public class ProductCategory: ITrackable
    {
        [Key]
        public int ProductCategroyId { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string CategoryName { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string CategoryDescription { get; set; }
        public ICollection<Product> Products { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset LastUpdateAt { get; set; }
        public string LastUpdateBy { get; set; }
    }
}
