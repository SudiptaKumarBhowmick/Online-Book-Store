using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Models.ApplicationModel
{
    public class ProductDetails
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductImageValue { get; set; }
        public int ProductCategory { get; set; }
    }
}
