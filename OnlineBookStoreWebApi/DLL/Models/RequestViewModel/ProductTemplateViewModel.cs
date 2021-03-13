using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Models.RequestViewModel
{
    public class ProductTemplateViewModel
    {
        public string TemplateProductCode { get; set; }
        public string TemplateProductName { get; set; }
        public decimal TemplateProductPrice { get; set; }
        public string TemplateProductImage { get; set; }
    }
}
