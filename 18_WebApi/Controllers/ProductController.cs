using _18_WebApi.DataContext;
using _18_WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _18_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductContext _context;
        public ProductController(ProductContext context)
        {
            _context = context;
        }

        // GET: Bir datayı getirmek için kullanılır. Örnek: /api/product/get-product
        [HttpGet("get-product")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        [HttpGet("get-product/{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
           var product = await _context.Products.FindAsync(id);
            
            if(product is null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost("add-product")] // POST: Yeni bir datayı eklemek için kullanılır. Örnek: /api/product/add-product
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            if (ProductExists(product.Id))
            {
                return BadRequest("Zaten ürün mevcut");
            }

            if(product is null)
            {
                return BadRequest("Ürün oluşuturulamadı.Request(İstek) hatalı");
            }

            _context.Products.AddAsync(product);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpPut("update-product")] // PUT: Var olan bir datayı güncellemek için kullanılır. Örnek: /api/product/update-product/1
        public async Task<ActionResult<Product>> PutProduct(int id, Product product)
        {
            if(id != product.Id)
            {
                return BadRequest("Ürün Id'si eşleşmiyor");
            }

            _context.Entry(product).State = EntityState.Modified; // Bu satır, Entity Framework Core'un ürünün durumunu "Modified" olarak işaretlemesini sağlar. Bu, veritabanında var olan bir kaydın güncelleneceğini belirtir.

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();

                }
                else
                {
                    throw;

                }
            }

            return NoContent(); // Bu, başarılı bir güncelleme işlemi sonrasında 204 No Content HTTP durum kodunu döndürür. Bu, isteğin başarılı olduğunu ancak döndürülecek bir içerik olmadığını belirtir.
        }


        [HttpPatch("upsert-product/{id}")] // PATCH: Var olan bir datayı güncellemek veya yoksa yeni bir data eklemek için kullanılır. Örnek: /api/product/upsert-product/1
        public async Task<ActionResult<Product>> PatchProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest("Ürün Id'si eşleşmiyor.");
            }

            // Veritabanında ürün var mı?
            var existingProduct = await _context.Products
                .FirstOrDefaultAsync(x => x.Id == id);

            if (existingProduct == null)
            {
                // Yeni ürün oluşturuluyor.
                // SQL Server Id'yi kendi üretecek.
                product.Id = 0;

                _context.Products.Add(product);

                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetProduct),
                    new { id = product.Id },
                    product
                );
            }

            // Ürün zaten varsa güncelle
            existingProduct.Title = product.Title;
            existingProduct.Price = product.Price;
            existingProduct.Description = product.Description;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("delete-product/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (!ProductExists(id))
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok("Ürününüz Silindi");
        }

        // Servis değil , sadece controller içinde kullanılacak bir method. Bu yüzden private olarak tanımlandı.
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
