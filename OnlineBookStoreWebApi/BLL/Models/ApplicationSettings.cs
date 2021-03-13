using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class ApplicationSettings
    {
        public string Jwt_Secret { get; set; }
        public string Client_URL { get; set; }
        public string Client_URL_HTTPS { get; set; }
    }
}
