using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Catalog.API.Entities;
using Catalog.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository productRepository, ILogger<CatalogController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productRepository.GetProducts();

            return Ok(products);
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType(typeof(Product), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetProductById(string id)
        {
            var product = await _productRepository.GetProduct(id);

            if (product == null)
            {
                _logger.LogError($"Product with id: {id} not found");
                return NotFound();
            }

            return Ok(product);
        }
        
        [Route("[action]/{name}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetProductByName(string name)
        {
            var products = await _productRepository.GetProductByName(name);

            return Ok(products);
        }
        
        [Route("[action]/{category}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetProductByCategory(string category)
        {
            var products = await _productRepository.GetProductByCategory(category);

            return Ok(products);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            await _productRepository.Create(product);

            return CreatedAtRoute("GetProduct", new {id = product.Id}, product);
        }
        
        [HttpPut]
        [ProducesResponseType(typeof(Product), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            return Ok(await _productRepository.Update(product));
        }
        
        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(typeof(Product), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProductById(string id)
        {
            return Ok(await _productRepository.Delete(id));
        }
    }
}