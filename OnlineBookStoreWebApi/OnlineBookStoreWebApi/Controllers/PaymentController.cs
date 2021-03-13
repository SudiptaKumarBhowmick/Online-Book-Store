using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Models;
using Braintree;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace OnlineBookStoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private IBraintreeConfiguration braintreeConfiguration;

        public static readonly TransactionStatus[] transactionSuccessStatuses =
            {
              TransactionStatus.AUTHORIZED,
              TransactionStatus.AUTHORIZING,
              TransactionStatus.SETTLED,
              TransactionStatus.SETTLING,
              TransactionStatus.SETTLEMENT_CONFIRMED,
              TransactionStatus.SETTLEMENT_PENDING,
              TransactionStatus.SUBMITTED_FOR_SETTLEMENT
             };

        public PaymentController(IConfiguration config)
        {
            braintreeConfiguration = new BraintreeConfiguration(config);
        }

        [HttpGet]
        [Route("getclienttoken")]
        public IActionResult GetClientToken()
        {
            var gateway = braintreeConfiguration.GetGateway();
            var clientToken = gateway.ClientToken.Generate();
            var clientTokenResponse = new ClientTokenResponse(clientToken);
            return new JsonResult(clientTokenResponse);
        }

        [HttpPost]
        [Route("createpurchase")]
        public IActionResult CreatePurchase([FromBody] NonceRequest nonceRequest)
        {
            string paymentStatus = string.Empty;
            var gateway = braintreeConfiguration.GetGateway();

            var request = new TransactionRequest
            {
                Amount = nonceRequest.ChargeAmount,
                PaymentMethodNonce = nonceRequest.Nonce,
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true
                }
            };

            Result<Transaction> result = gateway.Transaction.Sale(request);
            if (result.IsSuccess())
            {
                paymentStatus = "Succeded";
            }
            else
            {
                string errorMessage = "";
                foreach (ValidationError error in result.Errors.DeepAll())
                {
                    errorMessage += "Error: " + (int)error.Code + " " + error.Message + "\n";
                }
                paymentStatus = errorMessage;
            }
            return new JsonResult(paymentStatus);
        }
    }
}
