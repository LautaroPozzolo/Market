using Market.Model;

namespace Market.Services.Interfaces
{
    public interface IProductServices
    {
        Task<List<Product>> GetAll();
        Task<Product> ProductGetById(long? id);
        Task<Product> Create(Product product);
        Task<Product> Update(Product product);
        Task<string> Delete(long? id);
    }
}
