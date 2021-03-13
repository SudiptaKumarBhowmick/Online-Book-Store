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
using BLL.ServiceModelClass;
using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;

namespace OnlineBookStoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private AuthenticationContext _context;

        public ProductController(AuthenticationContext context)
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

        //Product details
        [HttpGet]
        [Route("ProductDetailsList")]
        public async Task<ActionResult<IEnumerable<ProductDetailsListModel>>> GetProductDetailsList()
        {
            try
            {
                var ProductListData = await
                    (from category in _context.ProductCategory
                     join product in _context.Products
                     on category.ProductCategroyId equals product.ProductCategroyId
                     select new ProductDetailsListModel()
                     {
                         ProductId = product.ProductId,
                         ProductName = product.ProductName,
                         ProductPrice = product.ProductPrice,
                         ProductImage = product.ProductImage,
                         CategoryId = product.ProductCategroyId,
                         ProductCategory = category.CategoryName
                     }).ToListAsync();
                return Ok(ProductListData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("ProductDetails")]
        public async Task<IActionResult> PostProductDetails(ProductDetails productDetailsModel)
        {
            try
            {
                AutoGenerateNumber _serviceGenAutoNum = new AutoGenerateNumber();
                decimal product_price = Convert.ToDecimal(productDetailsModel.ProductPrice);
                string product_code = _serviceGenAutoNum.GenAutoKeyNumber();
                var productDetails = new Product()
                {
                    ProductCode = product_code,
                    ProductName = productDetailsModel.ProductName,
                    ProductPrice = product_price,
                    ProductImage = productDetailsModel.ProductImageValue,
                    ProductCategroyId = productDetailsModel.ProductCategory
                };
                await _context.Products.AddAsync(productDetails);
                await _context.SaveChangesAsync();
                return Ok(productDetails);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProductDetailsData(int productId, ProductDetails productDetails)
        {
            try
            {
                var findProductDetails = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == productId);
                findProductDetails.ProductName = productDetails.ProductName;
                findProductDetails.ProductPrice = productDetails.ProductPrice;
                findProductDetails.ProductImage = productDetails.ProductImageValue ;
                findProductDetails.ProductCategroyId = productDetails.ProductCategory;
                var resultProductDetails = _context.Products.Update(findProductDetails);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("{productId}")]
        public async Task<ActionResult<Product>> DeleteProductDetails(int productId)
        {
            try
            {
                var productDetails = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == productId);
                var resultProductDetails = _context.Products.Remove(productDetails);
                await _context.SaveChangesAsync();
                return productDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //end product details

        [HttpGet]
        [Route("ProductTemplateList")]
        public async Task<ActionResult<IEnumerable<ProductTemplateViewModel>>> GetProductTemplateList(string product_category)
        {
            try
            {
                var productCatgId = (from p in _context.ProductCategory
                               where p.CategoryName == product_category
                               select p.ProductCategroyId).Single();
                return await _context.Products
                .Where(r => r.ProductCategroyId == Convert.ToInt32(productCatgId))
                .Select(p => new ProductTemplateViewModel
                {
                    TemplateProductName = p.ProductName,
                    TemplateProductCode = p.ProductCode,
                    TemplateProductImage = p.ProductImage,
                    TemplateProductPrice = p.ProductPrice
                }).Take(5).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("AllBookList")]
        public async Task<ActionResult<IEnumerable<AllProductListViewModel>>> GetAllBookList(string product_category)
        {
            try
            {
                var productCatgId = (from p in _context.ProductCategory
                                     where p.CategoryName == product_category
                                     select p.ProductCategroyId).Single();
                var all_book_list_info = await (from product in _context.Products
                              join productDescription in _context.ProductDescriptions on product.ProductId equals productDescription.ProductId
                              where product.ProductCategroyId == productCatgId
                              select new AllProductListViewModel()
                              {
                                  ProductCode = product.ProductCode,
                                  ProductImage = product.ProductImage,
                                  ProductName = product.ProductName,
                                  ProductDescription = productDescription.ProductSummary,
                                  AuthorName = productDescription.AuthorName,
                                  ProductPrice = product.ProductPrice
                              }).ToListAsync();
                return Ok(all_book_list_info);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("SingleProductDetails")]
        public async Task<ActionResult<IEnumerable<SingleProductDetailsViewModel>>> GetSingleProductDetails(string code)
        {
            try
            {
                var productId = (from p in _context.Products
                           where p.ProductCode == code
                           select p.ProductId).Single();
                var ProductDetailsInfoQuery = await
                    (from product in _context.Products
                     join ratings in _context.ProductReviews on product.ProductId equals ratings.ProductId
                     join instockavailable in _context.ProductInStocks on product.ProductId equals instockavailable.ProductId
                     join productdescription in _context.ProductDescriptions on product.ProductId equals productdescription.ProductId
                     where product.ProductId == productId
                     select new SingleProductDetailsViewModel()
                     {
                         BookTitle = product.ProductName,
                         AuthorName = productdescription.AuthorName,
                         CategoryName = productdescription.CategoryName,
                         Ratings = ratings.Review,
                         ProductPrice = product.ProductPrice,
                         InStockAvailable = instockavailable.InStock,
                         ProductImage = product.ProductImage
                     }).ToListAsync();
                return Ok(ProductDetailsInfoQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("SingleProductSpecificationSummary")]
        public async Task<ActionResult<IEnumerable<ProductSpecificationSummaryViewModel>>> GetSingleProductSpecificationSummary(string code)
        {
            try
            {
                var productId = (from p in _context.Products
                                 where p.ProductCode == code
                                 select p.ProductId).Single();
                return await _context.ProductDescriptions
                    .Where(m => m.ProductId == Convert.ToInt32(productId))
                    .Select(p => new ProductSpecificationSummaryViewModel
                    {
                        ProductSummary = p.ProductSummary,
                        Title = p.Title,
                        Publisher = p.PublisherName,
                        Edition = p.Edition,
                        NumOfPages = p.NumOfPages,
                        Country = p.Country,
                        Language = p.Language,
                        AuthorImage = p.AuthorImage,
                        AuthorName = p.AuthorName,
                        AuthorDescription = p.AuthorDescription
                    }).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        [HttpGet]
        [Route("CartListProductDetails")]
        public async Task<ActionResult<IEnumerable<CartListProductDetailsViewModel>>> GetCartListProductDetails(string codeList)
        {
            try
            {
                List<string> codes = codeList.Split(',').ToList<string>();
                Dictionary<string, int> counts = codes.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
                codes = codes.Distinct().ToList<string>();
                return await _context.Products
                    .Where(p => codes.Contains(p.ProductCode))
                .Select(p => new CartListProductDetailsViewModel
                {
                    ProductCode = p.ProductCode,
                    ProductQuantity = counts[p.ProductCode],
                    ProductImage = p.ProductImage,
                    ProductTitle = p.ProductName,
                    ProductPrice = p.ProductPrice
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        [HttpPost]
        [Route("ProceedToCheckOut")]
        public async Task<IActionResult> PostProceedToCheckOutDetails(List<OrderDetailsModel> OrderDetails)
        {
            try
            {
                AutoGenerateNumber _serviceGenAutoNum = new AutoGenerateNumber();
                //Ref order status code
                int orderStatusCode = Convert.ToInt32(_serviceGenAutoNum.GenStatusCode());
                var Ref_Order_Status_Code = new RefOrderStatusCodes()
                {
                    OrderStatusCode = orderStatusCode,
                    OrderStatusDescription = "Completed"
                };
                await _context.RefOrderStatusCodes.AddAsync(Ref_Order_Status_Code);
                await _context.SaveChangesAsync();

                //order
                DateTime orderDate = DateTime.Now;
                string address = (from u in _context.UserDetails
                                  where u.UserId == Convert.ToInt32(OrderDetails[0].UserId)
                                  select u.Address).Single();
                var order_details = new Orders()
                {
                    DateOrderPlaced = orderDate,
                    LocationOrderPlaced = address,
                    UserId = Convert.ToInt32(OrderDetails[0].UserId),
                    OrderStatusCode = orderStatusCode
                };
                await _context.Orders.AddAsync(order_details);
                await _context.SaveChangesAsync();

                //Ref Order Item Status Code
                int itemStatusCode = Convert.ToInt32(_serviceGenAutoNum.GenItemStatusCode());
                var ref_order_item_status_code = new RefOrderItemStatusCodes()
                {
                    OrderItemStatusCode = itemStatusCode,
                    OrderItemStatusDescription = "Delivered"
                };
                await _context.RefOrderItemStatusCodes.AddAsync(ref_order_item_status_code);
                await _context.SaveChangesAsync();

                //Order Item
                var pCode = OrderDetails.Select(p => p.ProductCode).ToList();
                var productId = (from p in _context.Products
                                 where pCode.Contains(p.ProductCode)
                                 select p.ProductId).ToList();
                Orders orders = new Orders()
                {
                    OrderId = _context.Orders
                    .FirstOrDefault(o => o.OrderStatusCode == orderStatusCode).OrderId
                };
                List<OrderItems> orderItemsList = new List<OrderItems>();
                for (int i = 0; i < OrderDetails.Count; i++)
                {
                    orderItemsList.Add(
                        new OrderItems
                        {
                            OrderItemQnt = Convert.ToInt32(OrderDetails[i].ProductQuantity),
                            OrderItemPrice = Convert.ToDecimal(OrderDetails[i].ProductPrice),
                            TotalAmount = Convert.ToDecimal(OrderDetails[0].TotalAmount),
                            ProductId = productId[i],
                            OrderId = orders.OrderId,
                            OrderItemStatusCode = itemStatusCode
                        });
                }
                foreach (var orderItem in orderItemsList)
                {
                    await _context.OrderItems.AddAsync(orderItem);
                    await _context.SaveChangesAsync();
                }

                //Ref Invoice Status Code
                int invoiceStatusCode = Convert.ToInt32(_serviceGenAutoNum.GenInvoiceStatusCode());
                var ref_invoice_status_code = new RefInvoiceStatusCode()
                {
                    InvoiceStatusCode = invoiceStatusCode,
                    InvoiceStatusDesc = "Issued"
                };
                await _context.RefInvoiceStatusCodes.AddAsync(ref_invoice_status_code);
                await _context.SaveChangesAsync();

                //Invoice
                string invoiceNumber = _serviceGenAutoNum.GenInvoiceNumber();
                DateTime invoiceDate = DateTime.Now;
                //List<Invoices> invoiceList = new List<Invoices>();
                //for (int i = 0; i < OrderDetails.Count; i++)
                //{
                //    invoiceList.Add(
                //        new Invoices
                //        {
                //            InvoiceNumber = invoiceNumber,
                //            InvoiceDate = invoiceDate,
                //            ItemName = OrderDetails[i].ProductTitle,
                //            ItemPrice = Convert.ToDecimal(OrderDetails[i].ProductPrice),
                //            TotalAmount = Convert.ToDecimal(OrderDetails[i].TotalAmount),
                //            OrderId = orders.OrderId,
                //            InvoiceStatusCode = invoiceStatusCode
                //        });
                //}
                var invoice_details = new Invoices()
                {
                    InvoiceNumber = invoiceNumber,
                    InvoiceStatusCode = invoiceStatusCode,
                    InvoiceDate = invoiceDate,
                    OrderId = orders.OrderId
                };
                await _context.Invoices.AddAsync(invoice_details);
                await _context.SaveChangesAsync();

                return Ok(orderStatusCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        [Route("SearchBoxText")]
        public async Task<ActionResult<IEnumerable<SearchBoxTextViewModel>>> GetSearchBoxTextResultList(string text)
        {
            try
            {
                var searchBoxResultList = await
                (from productDescription in _context.ProductDescriptions
                 join product in _context.Products on productDescription.ProductId equals product.ProductId
                 where productDescription.Title.ToLower().Contains(text.ToLower())
                 select new SearchBoxTextViewModel
                 {
                     ProductCode = product.ProductCode,
                     ProductTitle = productDescription.Title,
                     ProductImage = product.ProductImage,
                     AuthorName = productDescription.AuthorName
                 }).ToListAsync();
                return Ok(searchBoxResultList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        [HttpPost]
        [Route("TestPurpose")]
        public async Task<IActionResult> PostTestPurpose(WishlistModel wishlistmodel)
        {
            var wishListDetails = new Wishlist()
            {
                UserId = Convert.ToInt32(wishlistmodel.UserId),
                ProductCode = wishlistmodel.ProductCode
            };
            await _context.Wishlists.AddAsync(wishListDetails);
            await _context.SaveChangesAsync();
            return Ok(wishlistmodel);
        }
    }
}
