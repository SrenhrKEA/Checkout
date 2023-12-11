using Models;
using BusinessLogic;

namespace Services
{
    public class CheckoutManager
    {
        private readonly List<Product> _scannedProducts = new();
        private readonly CheapPriceCalculator _cheapPriceCalculator;
        private readonly ExpensivePriceCalculator _expensivePriceCalculator;

        public int ItemCount { get; private set; } = 0;

        public CheckoutManager(CheapPriceCalculator cheapCalculator, ExpensivePriceCalculator expensiveCalculator)
        {
            _cheapPriceCalculator = cheapCalculator;
            _expensivePriceCalculator = expensiveCalculator;
        }

        public void AddScannedProduct(Product product)
        {
            _scannedProducts.Add(product);
            ItemCount++;
        }

        public bool RemoveScannedProductAt(int index)
        {
            if (index >= 0 && index < _scannedProducts.Count)
            {
                _scannedProducts.RemoveAt(index);
                ItemCount--;
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
            ItemCount = 0;
        }
    }
}