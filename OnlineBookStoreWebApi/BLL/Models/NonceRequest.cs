using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class NonceRequest
    {
        public string Nonce { get; set; }
        public decimal ChargeAmount { get; set; }

        public NonceRequest(string nonce)
        {
            Nonce = nonce;
            ChargeAmount = ChargeAmount;
        }
    }
}
