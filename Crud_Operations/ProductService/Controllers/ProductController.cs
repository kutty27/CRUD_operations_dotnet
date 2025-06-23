using Microsoft.AspNetCore.Mvc;
using ProductService.Models;
using ProductService.Repositories;
//using ProductService.Services;

namespace ProductService.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _repository.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _repository.GetProductById(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpPost("add-product")]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            var createdProduct = await _repository.AddProduct(product);
            return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, createdProduct);
        }

        [HttpPut("update-product/{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
                return BadRequest();

            var updatedProduct = await _repository.UpdateProduct(product);
            return Ok(updatedProduct);
        }

        [HttpDelete("delete-product/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _repository.DeleteProduct(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
