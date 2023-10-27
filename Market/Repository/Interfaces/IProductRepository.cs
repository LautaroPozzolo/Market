using Market.Model;

namespace Market.Repository.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAll();
        Task<Product> ProductGetById(long? id);
        Task<Product> Create(Product product);
        Task<Product> Update(Product product);
        Task<string> Delete(long? id);
    }
}
