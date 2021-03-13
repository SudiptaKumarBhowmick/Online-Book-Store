using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DLL.DbContext;
using DLL.Models;
using DLL.Models.ApplicationModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace OnlineBookStoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProductCategoryController : ControllerBase
    {
        private AuthenticationContext _context;

        public ProductCategoryController(AuthenticationContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("ProductCategoryListData")]
        public async Task<ActionResult<IEnumerable<ProductCategoryModel>>> GetProductCategoryListData()
        {
            try
            {
                return await _context.ProductCategory
                .Select(p => new ProductCategoryModel
                {
                    ProductCategoryId = p.ProductCategroyId,
                    CategoryName = p.CategoryName,
                    CategoryDescription = p.CategoryDescription
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("SaveProductCategroy")]
        public async Task<IActionResult> SaveProductCategory(ProductCategoryModel productCategoryModel)
        {
            var productCategory = new ProductCategory()
            {
                CategoryName = productCategoryModel.CategoryName,
                CategoryDescription = productCategoryModel.CategoryDescription
            };
            await _context.ProductCategory.AddAsync(productCategory);
            await _context.SaveChangesAsync();

            return Ok(productCategory);
        }

        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateProductCategoryData(int categoryId, ProductCategoryModel productCategoryModel)
        {
            try
            {
                var productCategory = await _context.ProductCategory.FirstOrDefaultAsync(x => x.ProductCategroyId == categoryId);
                productCategory.CategoryName = productCategoryModel.CategoryName;
                productCategory.CategoryDescription = productCategoryModel.CategoryDescription;
                var resultProductCategory = _context.ProductCategory.Update(productCategory);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("{categoryId}")]
        public async Task<ActionResult<ProductCategory>> DeleteProductCategory(int categoryId)
        {
            try
            {
                var productCategory = await _context.ProductCategory.FirstOrDefaultAsync(x => x.ProductCategroyId == categoryId);
                var resultProductCategory = _context.ProductCategory.Remove(productCategory);
                await _context.SaveChangesAsync();
                return productCategory;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
