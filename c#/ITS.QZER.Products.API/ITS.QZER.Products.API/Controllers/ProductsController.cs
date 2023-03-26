using ITS.QZER.Products.API.Models;
using ITS.QZER.Products.API.Servicies;
using Microsoft.AspNetCore.Mvc;

namespace ITS.QZER.Products.API.Controllers
{

    private readonly IProductService _productsService;
    public class ProductsController : ControllerBase
    {
        public ProductController()
        {
            _productsService = productService;
        }
        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            var products = new List<Product>();
            for (int i = 0; i < 10; i++)
            {
                var product = new Product
                {
                    Id = i,
                    Name = $"Prodotto{i}",
                    Price = 12.3M,
                };
                products.Add(product);
            }
            return products;
        }
        [HttpGet("{id}")]
        public Product GetProduct(int id)
        {
            return new Product
            {
                Id = id,
                Name = $"Prodotto{id}",
                Price = 12.3M
            };
        }
    }
}
