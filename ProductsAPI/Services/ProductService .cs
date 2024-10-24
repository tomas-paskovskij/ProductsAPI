using ProductsAPI.Models;
using ProductsAPI.Repositories;

namespace ProductsAPI.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> CreateProductAsync(Product product);
        Task UpdateProductAsync(int id, Product product);
        Task DeleteProductAsync(int id);
    }

    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
                throw new KeyNotFoundException($"Product with ID {id} not found");
            return product;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            if (string.IsNullOrEmpty(product.Name))
                throw new ArgumentException("Product name cannot be empty");

            if (product.Price <= 0)
                throw new ArgumentException("Price must be greater than zero");

            return await _repository.CreateAsync(product);
        }

        public async Task UpdateProductAsync(int id, Product product)
        {
            if (id != product.Id)
                throw new ArgumentException("ID mismatch");

            var exists = await _repository.ExistsAsync(id);
            if (!exists)
                throw new KeyNotFoundException($"Product with ID {id} not found");

            await _repository.UpdateAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            var exists = await _repository.ExistsAsync(id);
            if (!exists)
                throw new KeyNotFoundException($"Product with ID {id} not found");

            await _repository.DeleteAsync(id);
        }
    }
}
