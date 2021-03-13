using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class ClientTokenResponse
    {
        public string Token { get; set; }

        public ClientTokenResponse(string token)
        {
            Token = token;
        }
    }
}
