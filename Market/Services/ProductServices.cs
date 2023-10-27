using Market.Model;
using Market.Repository.Interfaces;
using Market.Services.Interfaces;

namespace Market.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IProductRepository _repository;

        public ProductServices(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Product> Create(Product product)
        {
            return await _repository.Create(product);
        }

        public async Task<string> Delete(long? id)
        {
            return await _repository.Delete(id);
        }

        public async Task<List<Product>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Product> ProductGetById(long? id)
        {
            return await _repository.ProductGetById(id);
        }

        public async Task<Product> Update(Product product)
        {
            return await _repository.Update(product);
        }
    }
}
