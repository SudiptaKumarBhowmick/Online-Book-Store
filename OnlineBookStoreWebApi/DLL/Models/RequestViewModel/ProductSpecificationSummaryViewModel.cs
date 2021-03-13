using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Models.RequestViewModel
{
    public class ProductSpecificationSummaryViewModel
    {
        public string ProductSummary { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public string Edition { get; set; }
        public int NumOfPages { get; set; }
        public string Country { get; set; }
        public string Language { get; set; }
        public string AuthorImage { get; set; }
        public string AuthorName { get; set; }
        public string AuthorDescription { get; set; }
    }
}
