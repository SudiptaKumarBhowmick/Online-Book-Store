using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Models.RequestViewModel
{
    public class AllProductListViewModel
    {
        public string ProductCode { get; set; }
        public string ProductImage { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string AuthorName { get; set; }
        public decimal ProductPrice { get; set; }

    }
}
