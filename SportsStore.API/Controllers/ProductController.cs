using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsStore.Models;
using SportsStore.Models.BindingTargets;

namespace SportsStore.Controllers {
    [Route ("api/[controller]")]
    public class ProductController : Controller {
        private readonly StoreAppContext _context;
        public ProductController (StoreAppContext context) {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Product> GetAll (string category, string search, bool? related) {
            IQueryable<Product> products = _context.Products;

            if (!string.IsNullOrWhiteSpace (category)) {
                products = products.Where (p => p.Category.ToLower ().Contains (category.ToLower ()));
            }
            if (!string.IsNullOrWhiteSpace (search)) {
                    products = products.Where (p => p.Name.ToLower ().Contains (search.ToLower ()) ||
                        p.Description.ToLower ().Contains (search.ToLower ()));
                }
            if (related != null && related == true) {
                products = products.Include (p => p.Supplier).Include (p => p.Ratings);
                var data = products.ToList ();
                data.ForEach (p => {
                    if (p.Supplier != null) {
                        p.Supplier.Products = null;
                    }
                    if (p.Ratings != null) {
                        p.Ratings.ForEach (r => r.Product = null);
                    }
                });
                return data;
            } else {
                return products;
            }
        }

        [HttpGet ("{id}")]
        [Route ("getProduct")]
        public IActionResult GetById (long id) {
            var result = _context.Products
                .Include (p => p.Supplier)
                .Include (p => p.Ratings)
                .FirstOrDefault (t => t.ProductId == id);

            if (result != null) {
                if (result.Supplier != null) {
                    result.Supplier.Products = result.Supplier.Products.Select (
                        p => p = new Product {
                            ProductId = p.ProductId,
                                Name = p.Name,
                                Category = p.Category,
                                Price = p.Price
                        }
                    );
                }
                if (result.Ratings != null) {
                    foreach (var r in result.Ratings) {
                        r.Product = null;
                    }
                }
                return new ObjectResult (result);
            } else {
                return NotFound ();
            }

        }

        [HttpPost]
        [Route ("addProduct")]
        public IActionResult Create ([FromBody] ProductData productData) {
            if (ModelState.IsValid){
                Product product = productData.Product;
                if (product.Supplier != null && product.Supplier.SupplierId != 0){
                    _context.Attach(product.Supplier);
                }
                _context.Add(product);
                _context.SaveChanges();
                return Ok(product.ProductId);
            }else {
                return BadRequest(ModelState);
            }
        }

        [HttpPut ("{id}")]
        [Route ("updateProduct")]
        public IActionResult Update (long id, [FromBody] Product item) {
            if (item == null || id == 0) {
                return BadRequest ();
            }
            var Product = _context.Products.FirstOrDefault (t => t.ProductId == id);
            if (Product == null) {
                return NotFound ();
            }
            Product.Name = item.Name;
            Product.ProductId = item.ProductId;
            Product.Category = item.Category;
            Product.Description = item.Description;
            Product.Price = item.Price;
            _context.Products.Update (Product);
            _context.SaveChanges ();
            return Ok (new { message = "Prodcut is updated Successfully..." });
        }

        [HttpDelete ("{id}")]
        [Route ("deleteProduct")]
        public IActionResult Delete (long id) {
            var Prodcut = _context.Products.FirstOrDefault (t => t.ProductId == id);
            if (Prodcut == null) {
                return NotFound ();
            }
            _context.Products.Remove (Prodcut);
            _context.SaveChanges ();

            return Ok (new { message = "Product is deleted Successfully" });
        }
    }
}