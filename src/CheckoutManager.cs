using Models;
using BusinessLogic;

namespace Services
{
    public class CheckoutManager
    {
        public int ItemCount = 0;
        private List<Product> _scannedProducts = new();
        private CheapPriceCalculator _cheapPriceCalculator;
        private ExpensivePriceCalculator _expensivePriceCalculator;

        public CheckoutManager(CheapPriceCalculator cheapCalculator, ExpensivePriceCalculator expensiveCalculator)
        {
            _cheapPriceCalculator = cheapCalculator;
            _expensivePriceCalculator = expensiveCalculator;
        }

        public void AddScannedProduct(Product product)
        {
            _scannedProducts.Add(product);
            ItemCount += 1;
        }

        public bool RemoveScannedProductAt(int index)
        {
            if (index >= 0 && index < _scannedProducts.Count)
            {
                _scannedProducts.RemoveAt(index);
                ItemCount -= 1;
                return true;
            }
            return false;
        }

        public IEnumerable<Product> GetScannedProducts()
        {
            return _scannedProducts;
        }

        public string GetTotalPrice()
        {
            return _cheapPriceCalculator.CalculateTotalPrice(_scannedProducts);
        }

        public string CompleteCheckout()
        {
            return _expensivePriceCalculator.CalculateTotalPrice(_scannedProducts);
        }

        public void EmptyBin()
        {
            _scannedProducts.Clear();
        }
    }
}
