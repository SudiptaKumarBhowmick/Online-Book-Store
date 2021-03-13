using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Models.RequestViewModel
{
    public class CartListProductDetailsViewModel
    {
        public string ProductCode { get; set; }
        public int ProductQuantity { get; set; }
        public string ProductImage { get; set; }
        public string ProductTitle { get; set; }
        public decimal ProductPrice { get; set; }
    }
}
