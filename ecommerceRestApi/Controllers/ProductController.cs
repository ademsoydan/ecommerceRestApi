using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerceRestApi.Models;
using ecommerceRestApi.Models.Dto;
using ecommerceRestApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ecommerceRestApi.Controllers
{
    
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly ProductService productService;

        public ProductController(ProductService productService)
        {
            this.productService = productService;
        }
        // GET: api/values
        [HttpGet]
        public IEnumerable<ProductDto> Get()
        {
            return productService.getAllProducts();
        }

        // POST: api/product/ids
        [HttpPost("ids")]
        public ActionResult<IEnumerable<ProductDto>> GetProductsByIds([FromBody] ProductIdsRequest request)
        {
            if (request == null || request.Ids == null || !request.Ids.Any())
            {
                return BadRequest("Product IDs are required.");
            }

            var products = productService.GetProductsByIds(request.Ids);
            if (products == null || !products.Any())
            {
                return NotFound("No products found for the provided IDs.");
            }

            return Ok(products);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public List<Product> Get(int id)
        {
           return  productService.getProductsByAdminId(id);
        }

        // POST api/values
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody]AddProductRequest request)
        {
            productService.AddProduct(request);
            return Ok("başarılı");
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        [HttpGet("haveStock/{productId}")]
        public bool haveStock(int productId)
        {
            return productService.haveStock(productId);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

