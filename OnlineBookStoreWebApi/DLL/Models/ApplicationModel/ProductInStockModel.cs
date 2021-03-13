using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Models.ApplicationModel
{
    public class ProductInStockModel
    {
        public int StockId { get; set; }
        public int ProductNameId { get; set; }
        public string ProductInStock { get; set; }
        public int ProductQuantity { get; set; }
    }
}
