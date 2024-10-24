using ProductsAPI.Models;

namespace ProductsAPI.Client
{
    public class ProductApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ProductApiClient(string baseUrl)
        {
            _baseUrl = baseUrl;
            _httpClient = new HttpClient();
        }

        // GET all products
        public async Task<List<Product>> GetAllProductsAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/products");
            response.EnsureSuccessStatusCode();
            var products = await response.Content.ReadFromJsonAsync<List<Product>>();
            return products;
        }

        // GET single product
        public async Task<Product> GetProductByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/products/{id}");
            response.EnsureSuccessStatusCode();
            var product = await response.Content.ReadFromJsonAsync<Product>();
            return product;
        }

        // POST new product
        public async Task<Product> CreateProductAsync(Product product)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/products", product);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Product>();
        }

        // PUT update product
        public async Task UpdateProductAsync(int id, Product product)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/api/products/{id}", product);
            response.EnsureSuccessStatusCode();
        }

        // DELETE product
        public async Task DeleteProductAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/products/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
