using Microsoft.EntityFrameworkCore;
using Market.Data;
using Market.Model;
using Market.Repository.Interfaces;

namespace Market.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAll()
        {
            return await _context.Product.Take(200).ToListAsync();
        }

        public async Task<Product> ProductGetById(long? id)
        {

            if (id == null || _context.Product == null)
            {
                throw new Exception("An error occurred...");
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                throw new Exception("An error occurred...");
            }

            return product;
        }

        public async Task<Product> Create(Product product)
        {
            if (product == null)
            {
                throw new Exception("An error occurred...");
            }

            _context.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<string> Delete(long? id)
        {

            if (id == null || _context.Product == null)
            {
                return "Id: " + id + " was not found.";
            }

            var product = await _context.Product.FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return "the product: " + id + " was not found.";
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return "The Product was successfully removed.";
        }

        public async Task<Product> Update(Product request)
        {
            if (request == null)
            {
                throw new Exception("An error occurred...");
            }

            _context.Entry(request).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            //TODO: Return a call for the new prod
            return request;
        }
    }
}
