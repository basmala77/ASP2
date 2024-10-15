using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("Products")]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbcontext _dbcontext;

        public ProductsController(ApplicationDbcontext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        [HttpGet]
        [Route("")]
        public ActionResult<IEnumerable<Product>> Get() {
            var record = _dbcontext.Set<Product>().ToList();
            return Ok(record);
        }
        [HttpGet]
        [Route("{id}")]
        public ActionResult<Product> Get(int id)
        {
            var record = _dbcontext.Set<Product>().Find(id);
            return Ok(record);
        }
        [HttpPost]
        [Route("")]
        public ActionResult<int> CreateProduct(Product product)
        {
            product.Id = 0;
            _dbcontext.Set<Product>().Add(product);
            _dbcontext.SaveChanges();
            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }
        [HttpPut]
        [Route("")]
        public ActionResult UpdateProduct(Product product)
        {
            var existingProduct = _dbcontext.Set<Product>().Find(product.Id);
            existingProduct.Name = product.Name;
            existingProduct.Sku = product.Sku;
            _dbcontext.Set<Product>().Update(existingProduct);
            _dbcontext.SaveChanges();
            return Ok();  
        }
        [HttpDelete]
        [Route("")]
        public ActionResult DeleteProduct(int id) {
            var existingProduct = _dbcontext.Set<Product>().Find(id);
            _dbcontext.Set<Product>().Remove(existingProduct);
            _dbcontext.SaveChanges();
            return Ok();
        }
    }
}
