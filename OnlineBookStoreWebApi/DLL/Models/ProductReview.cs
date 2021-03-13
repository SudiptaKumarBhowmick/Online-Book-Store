using DLL.Models.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DLL.Models
{
    public class ProductReview: ITrackable
    {
        [Key]
        public int ProductReviewId { get; set; }
        public int UserId { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string UserName { get; set; }
        [Column(TypeName = "varchar(500)")]
        public string Comment { get; set; }
        public int Review { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Product Product { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset LastUpdateAt { get; set; }
        public string LastUpdateBy { get; set; }
    }
}
