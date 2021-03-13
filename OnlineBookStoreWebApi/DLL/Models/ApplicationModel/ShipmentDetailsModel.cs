using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Models.ApplicationModel
{
    public class ShipmentDetailsModel
    {
        public string FullName { get; set; }
        public string AddressLineOne { get; set; }
        public string AddressLineTwo { get; set; }
        public string City { get; set; }
        public string StateProvinceRegion { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string ShipmentDate { get; set; }
        public string userId { get; set; }
        public string OrderStatusCode { get; set; }
    }
}
