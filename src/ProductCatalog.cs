using Models;

namespace Services
{
    public class ProductCatalog
    {
        private Dictionary<char, Product> _products = new();

        public void AddProduct(Product product) => _products[product.Code] = product;

        public Product GetProduct(char code)
        {
            if (_products.TryGetValue(code, out Product? product))
            {
                return product;
            }

            throw new KeyNotFoundException($"Product with code '{code}' not found.");
        }
    }
}
