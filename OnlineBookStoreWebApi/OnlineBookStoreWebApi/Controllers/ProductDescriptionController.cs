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
    public class ProductDescriptionController : ControllerBase
    {
        private AuthenticationContext _context;

        public ProductDescriptionController(AuthenticationContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("ProductCategoryList")]
        public async Task<ActionResult<IEnumerable<ProductCategoryViewModel>>> GetProductCategoryList()
        {
            try
            {
                return await _context.ProductCategory
                .Select(p => new ProductCategoryViewModel
                {
                    ProductCategroyId = p.ProductCategroyId,
                    CategoryName = p.CategoryName
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        [Route("ProductList")]
        public async Task<ActionResult<IEnumerable<ProductListViewModel>>> GetProductList(string productCategoryId)
        {
            try
            {
                return await _context.Products
                .Where(p => p.ProductCategroyId == Convert.ToInt32(productCategoryId))
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

        [HttpGet]
        [Route("ProductDescriptionListData")]
        public async Task<ActionResult<IEnumerable<ProductDescriptionListModel>>> GetProductDescriptionListData()
        {
            try
            {
                var ProductDescriptionList = await
                    (from productDescription in _context.ProductDescriptions
                     join product in _context.Products
                     on productDescription.ProductId equals product.ProductId
                     select new ProductDescriptionListModel()
                     {
                         ProductDescId = productDescription.ProductDescId,
                         ProductCategoryId = product.ProductCategroyId,
                         ProductCategory = productDescription.CategoryName,
                         ProductId = productDescription.ProductId,
                         ProductName = productDescription.Title,
                         AuthorName = productDescription.AuthorName,
                         AuthorDescription = productDescription.AuthorDescription,
                         AuthorImageValue = productDescription.AuthorImage,
                         ProductSummary = productDescription.ProductSummary,
                         PublisherName = productDescription.PublisherName,
                         Edition = productDescription.Edition,
                         NumofPages = productDescription.NumOfPages,
                         Country = productDescription.Country,
                         Language = productDescription.Language,
                     }).ToListAsync();
                return Ok(ProductDescriptionList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("SaveProductDescription")]
        public async Task<IActionResult> PostProductDescription(ProductDescriptionModel productDescriptionModel)
        {
            try
            {
                var productDescription = new ProductDescription()
                {
                    Title = productDescriptionModel.ProductName,
                    CategoryName = productDescriptionModel.ProductCategory,
                    AuthorName = productDescriptionModel.AuthorName,
                    AuthorDescription = productDescriptionModel.AuthorDescription,
                    AuthorImage = productDescriptionModel.AuthorImageValue,
                    ProductSummary = productDescriptionModel.ProductSummary,
                    PublisherName = productDescriptionModel.PublisherName,
                    Edition = productDescriptionModel.Edition,
                    NumOfPages = productDescriptionModel.NumofPages,
                    Country = productDescriptionModel.Country,
                    Language = productDescriptionModel.Language,
                    ProductId = productDescriptionModel.ProductId
                };
                await _context.ProductDescriptions.AddAsync(productDescription);
                await _context.SaveChangesAsync();
                return Ok(productDescription);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("{productDescId}")]
        public async Task<IActionResult> UpdateProductDescriptionData(int productDescId, ProductDescriptionModel productDescriptionModel)
        {
            try
            {
                var productDescriptionDetails = await _context.ProductDescriptions.FirstOrDefaultAsync(x => x.ProductDescId == productDescId);
                productDescriptionDetails.AuthorName = productDescriptionModel.AuthorName;
                productDescriptionDetails.AuthorDescription = productDescriptionModel.AuthorDescription;
                productDescriptionDetails.AuthorImage = productDescriptionModel.AuthorImageValue;
                productDescriptionDetails.ProductSummary = productDescriptionModel.ProductSummary;
                productDescriptionDetails.PublisherName = productDescriptionModel.PublisherName;
                productDescriptionDetails.Edition = productDescriptionModel.Edition;
                productDescriptionDetails.NumOfPages = productDescriptionModel.NumofPages;
                productDescriptionDetails.Country = productDescriptionModel.Country;
                productDescriptionDetails.Language = productDescriptionModel.Language;
                var resultProductStock = _context.ProductDescriptions.Update(productDescriptionDetails);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
