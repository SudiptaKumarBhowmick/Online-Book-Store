using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Models.ApplicationModel
{
    public class ProductDetailsListModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductImage { get; set; }
        public int CategoryId { get; set; }
        public string ProductCategory { get; set; }
    }
}
