using Models;
using BusinessLogic;

namespace Services
{
    public class CheckoutManager
    {
        private int counter = 0;
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
            counter += 1;
            UpdateDisplayPrice();
        }

        public void DisplayScannedProducts()
        {
            for (int i = 0; i < _scannedProducts.Count; i++)
            {
                Console.WriteLine($"{i}: Product Code - {_scannedProducts[i].Code}, Price - {_scannedProducts[i].Price}");
            }
        }

        public bool RemoveScannedProductAt(int index)
        {
            if (index >= 0 && index < _scannedProducts.Count)
            {
                _scannedProducts.RemoveAt(index);
                counter -= 1;
                UpdateDisplayPrice();
                return true;
            }
            return false;
        }

        private void UpdateDisplayPrice()
        {
            decimal totalPrice = _cheapPriceCalculator.CalculateTotalPrice(_scannedProducts);
            Console.WriteLine($"Number of items: {counter} || Total Price: {totalPrice.ToString("F2")}");
        }

        public decimal CompleteCheckout()
        {
            decimal totalPrice = _expensivePriceCalculator.CalculateTotalPrice(_scannedProducts);
            return totalPrice;
        }
    }
}
