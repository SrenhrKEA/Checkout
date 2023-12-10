using Models;

namespace Services
{
    public class ProductCatalog
    {
        private Dictionary<char, Product> _products = new Dictionary<char, Product>();

        public void AddProduct(Product product)
        {
            _products[product.Code] = product;
        }

        public Product GetProduct(char code)
        {
            if (_products.TryGetValue(code, out Product product))
            {
                return product;
            }

            return null; // or throw an exception if you prefer
        }

        // Additional methods like UpdateProduct, DeleteProduct, etc., can be added as needed.


        //LINQ METHODS

        public IEnumerable<Product> GetProductsByGroup(int group)
        {
            return _products.Values.Where(product => product.Group == group);
        }

        public IEnumerable<Product> GetProductsSortedByPrice()
        {
            return _products.Values.OrderBy(product => product.Price);
        }

        // More LINQ-based methods can be added as needed
    }


}