using DLL.Models.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DLL.Models
{
    public class ProductDescription: ITrackable
    {
        [Key]
        public int ProductDescId { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Title { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string CategoryName { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string AuthorName { get; set; }
        [Column(TypeName = "varchar(1000)")]
        public string AuthorDescription { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string AuthorImage { get; set; }
        [Column(TypeName = "varchar(1000)")]
        public string ProductSummary { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string PublisherName { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Edition { get; set; }
        public int NumOfPages { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string Country { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string Language { get; set; }
        public int ProductId { get; set; }
        public Product Products { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset LastUpdateAt { get; set; }
        public string LastUpdateBy { get; set; }
    }
}
