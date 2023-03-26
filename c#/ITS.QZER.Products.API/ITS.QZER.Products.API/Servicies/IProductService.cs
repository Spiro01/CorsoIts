using ITS.QZER.Products.API.Models;

namespace ITS.QZER.Products.API.Servicies
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product GetById(int id);
    }
}
