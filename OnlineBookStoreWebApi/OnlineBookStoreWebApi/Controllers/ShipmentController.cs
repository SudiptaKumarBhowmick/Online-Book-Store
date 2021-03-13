using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using BLL.ServiceModelClass;
using DLL.DbContext;
using DLL.Models;
using DLL.Models.ApplicationModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineBookStoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentController : ControllerBase
    {
        private AuthenticationContext _context;
        public ShipmentController(AuthenticationContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("SaveShipmentDetails")]
        public async Task<IActionResult> PostShipmentDetails(ShipmentDetailsModel shipmentDetailsModel)
        {
            try
            {
                AutoGenerateNumber autoGenerateNumber = new AutoGenerateNumber();
                int orderId = (from s in _context.Orders
                               where s.UserId == Convert.ToInt32(shipmentDetailsModel.userId) &&
                               s.OrderStatusCode == Convert.ToInt32(shipmentDetailsModel.OrderStatusCode)
                               select s.OrderId).Single();
                int invoiceId = (from i in _context.Invoices
                                 where i.OrderId == orderId
                                 select i.InvoiceId).Single();
                string shipmentTrackNum = autoGenerateNumber.GenShipmentTrackNum();
                var shipmentDetails = new Shipment()
                {
                    ShipmentTrackNum = shipmentTrackNum,
                    FullName = shipmentDetailsModel.FullName,
                    AddressLineOne = shipmentDetailsModel.AddressLineOne,
                    AddressLineTwo = shipmentDetailsModel.AddressLineTwo,
                    City = shipmentDetailsModel.City,
                    State = shipmentDetailsModel.StateProvinceRegion,
                    Zip = shipmentDetailsModel.Zip,
                    Country = shipmentDetailsModel.Country,
                    ShipmentDate = Convert.ToDateTime(shipmentDetailsModel.ShipmentDate),
                    OrderId = orderId,
                    InvoiceId = invoiceId
                };
                await _context.Shipments.AddAsync(shipmentDetails);
                await _context.SaveChangesAsync();

                return Ok(shipmentDetails);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
