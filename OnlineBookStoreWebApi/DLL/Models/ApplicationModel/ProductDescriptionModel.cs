using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Models.ApplicationModel
{
    public class ProductDescriptionModel
    {
        public int ProductDescId { get; set; }
        public string ProductCategory { get; set; }
        public string ProductName { get; set; }
        public string AuthorName { get; set; }
        public string AuthorDescription { get; set; }
        public string AuthorImageValue { get; set; }
        public string ProductSummary { get; set; }
        public string PublisherName { get; set; }
        public string Edition { get; set; }
        public int NumofPages { get; set; }
        public string Country { get; set; }
        public string Language { get; set; }
        public int ProductId { get;set; }
    }
}
