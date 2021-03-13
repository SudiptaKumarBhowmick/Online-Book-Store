using Braintree;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public interface IBraintreeConfiguration
    {
        IBraintreeGateway CreateGateway();
        string GetConfigurationSetting(string setting);
        IBraintreeGateway GetGateway();
    }
}
