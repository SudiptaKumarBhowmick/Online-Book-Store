using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Models.ApplicationModel
{
    public class PaymentDetailsModel
    {
        public string Name { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string FullName { get; set; }
        public string AddressLineOne { get; set; }
        public string AddressLineTwo { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string userId { get; set; }
    }
}
