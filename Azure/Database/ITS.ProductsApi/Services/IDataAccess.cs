using ITS.ProductsApi.Models;

namespace ITS.ProductsApi.Services;

public interface IDataAccess
{
    public Task<IEnumerable<Products>> GetProductsAsync();
    public Task<Products> GetProductAsync(int id);
    public Task InsertProductAsync(Products product);
}