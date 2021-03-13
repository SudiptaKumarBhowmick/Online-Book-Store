using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DLL.DbContext;
using DLL.Models;
using DLL.Models.ApplicationModel;
using DLL.Models.RequestViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace OnlineBookStoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductInStockController : ControllerBase
    {
        private AuthenticationContext _context;

        public ProductInStockController(AuthenticationContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("ProductInStockListData")]
        public async Task<ActionResult<IEnumerable<ProductInStockModel>>> GetProductInStockListData()
        {
            try
            {
                var ProductInStockList = await
                    (from stock in _context.ProductInStocks
                     join product in _context.Products
                     on stock.ProductId equals product.ProductId
                     select new ProductInStockViewModel()
                     {
                         ProductStockId = stock.ProductStockId,
                         ProductNameId = stock.ProductId,
                         ProductName = product.ProductName,
                         ProductInStock = stock.InStock,
                         ProductQuantity = stock.Quantity
                     }).ToListAsync();
                return Ok(ProductInStockList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("ProductList")]
        public async Task<ActionResult<IEnumerable<ProductListViewModel>>> GetProductList()
        {
            try
            {
                return await _context.Products
                .Select(p => new ProductListViewModel
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        [Route("SaveProductStockDetails")]
        public async Task<IActionResult> SaveProductStockDetails(ProductInStockModel productInStockModel)
        {
            var productInStock = new ProductInStock()
            {
                InStock = productInStockModel.ProductInStock,
                Quantity = productInStockModel.ProductQuantity,
                ProductId = productInStockModel.ProductNameId
            };
            await _context.ProductInStocks.AddAsync(productInStock);
            await _context.SaveChangesAsync();

            return Ok(productInStock);
        }

        [HttpPut("{productStockId}")]
        public async Task<IActionResult> UpdateProductStockData(int productStockId, ProductInStockModel productInStockModel)
        {
            try
            {
                var productStockDetails = await _context.ProductInStocks.FirstOrDefaultAsync(x => x.ProductStockId == productStockId);
                productStockDetails.InStock = productInStockModel.ProductInStock;
                productStockDetails.Quantity = productInStockModel.ProductQuantity;
                productStockDetails.ProductId = productInStockModel.ProductNameId;
                var resultProductStock = _context.ProductInStocks.Update(productStockDetails);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("{productStockId}")]
        public async Task<ActionResult<ProductInStock>> DeleteProductStockData(int productStockId)
        {
            try
            {
                var productStockDetails = await _context.ProductInStocks.FirstOrDefaultAsync(x => x.ProductStockId == productStockId);
                var resultProductStock = _context.ProductInStocks.Remove(productStockDetails);
                await _context.SaveChangesAsync();
                return productStockDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
