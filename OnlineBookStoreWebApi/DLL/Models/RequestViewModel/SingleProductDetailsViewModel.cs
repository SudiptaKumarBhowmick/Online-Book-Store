using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Models.RequestViewModel
{
    public class SingleProductDetailsViewModel
    {
        public string BookTitle { get; set; }
        public string AuthorName { get; set; }
        public string CategoryName { get; set; }
        public int Ratings { get; set; }
        public decimal ProductPrice { get; set; }
        public string InStockAvailable { get; set; }
        public string ProductImage { get; set; }
    }
}
