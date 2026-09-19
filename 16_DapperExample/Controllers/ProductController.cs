using _16_DapperExample.Data;
using _16_DapperExample.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace _16_DapperExample.Controllers
{
    public class ProductController : Controller
    {
        private readonly DapperContext _context;

        public ProductController(DapperContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            // ürünleri ve kategori adlarını getiren sql sorgusu
            var query = "select * from Product join Category on Category.CategoryId = Product.CategoryId";

            using (var connection = _context.CreateConnection())
            {
                // Çoklu tablo sorgusu
                var products = await connection.QueryAsync
                    <Product, Category, Product>(
                    query,
                    (product, category) =>
                    {
                        product.Category = category;
                        return product;
                    },
                    splitOn: "CategoryId"
                );

                return View(products.ToList());
            }

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            var query = "insert into Product (Name, Price, CategoryId) values (@Name, @Price, @CategoryId)";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, product);
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var query = "select * from Product where ProductId = @Id";
            using (var connection = _context.CreateConnection())
            {
                // Dapper ile ürün sorgulama
                var product = await connection.QuerySingleOrDefaultAsync<Product>
                    (query, new { Id = id });
                if (product is null)
                {
                    return NotFound();
                }

                return View(product);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            var query = "update Product set Name = @Name,Price = @Price,CategoryId = @CategoryId where ProductId = @ProductId";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, product);
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            var query = "select * from Product where ProductId = @Id";
            using (var connection = _context.CreateConnection())
            {
                var product = await connection.QuerySingleOrDefaultAsync<Product>
                    (query, new { Id = id });
                if (product is null)
                {
                    return NotFound();
                }
                return View(product);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var query = "select * from Product where ProductId = @Id";
            using (var connection = _context.CreateConnection())
            {
                var product = await connection.QuerySingleOrDefaultAsync<Product>
                    (query, new { Id = id });
                if (product is null)
                {
                    return NotFound();
                }
                return View(product);
            }
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var query = "delete from Product where ProductId = @Id";
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.ExecuteAsync(query, new { Id = id });

                if(result > 0)
                {
                    ViewBag.Message = "Product deleted successfully.";
                }
                else
                {
                    ViewBag.Message = "Product not found or could not be deleted.";
                }

                return View("DeleteResult");
            }
        }
    }
}
