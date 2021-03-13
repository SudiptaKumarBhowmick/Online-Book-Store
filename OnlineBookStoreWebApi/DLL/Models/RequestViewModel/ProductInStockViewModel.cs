using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Models.RequestViewModel
{
    public class ProductInStockViewModel
    {
        public int ProductStockId { get; set; }
        public int ProductNameId { get; set; }
        public string ProductName { get; set; }
        public string ProductInStock { get; set; }
        public int ProductQuantity { get; set; }
    }
}
