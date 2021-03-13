using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class CheckoutModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public decimal Price { get; set; }
        public string PaymentMethodNonce { get; set; }
    }
}
