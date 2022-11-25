using ITS.ProductsApi.Models;
using ITS.ProductsApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITS.ProductsApi.Controllers
{



    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IDataAccess _dataAccess;

        public ProductsController(IDataAccess dataAccess) => _dataAccess = dataAccess;


        [HttpGet]
        public Task<IEnumerable<Products>> GetAll()
        {
            return _dataAccess.GetProductsAsync();
        }

        [HttpGet("{id}")]
        public Task<Products> Get(int id)
        {
            return _dataAccess.GetProductAsync(id);
        }

        [HttpPost]
        public Task Insert(Products products)
        {
            return _dataAccess.InsertProductAsync(products);
        }

    }
}
