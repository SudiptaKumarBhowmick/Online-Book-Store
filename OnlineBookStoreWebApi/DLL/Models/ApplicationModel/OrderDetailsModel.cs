using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Models.ApplicationModel
{
    public class OrderDetailsModel
    {
        public string ProductCode { get; set; }
        public string ProductTitle { get; set; }
        public string ProductPrice { get; set; }
        public string ProductQuantity { get; set; }
        public string TotalAmount { get; set; }
        public string UserId { get; set; }
    }
}
