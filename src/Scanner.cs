using Models;

namespace Services
{
    public class Scanner
    {
        private readonly ProductCatalog _productCatalog;
        public delegate void ProductScannedEventHandler(Product product);
        public event ProductScannedEventHandler? ProductScanned;
        
        public Scanner(ProductCatalog productCatalog)
        {
            _productCatalog = productCatalog;
        }

        public async Task ScanAsync(char productCode)
        {
            await Task.Delay(500);

            Product product = _productCatalog.GetProduct(productCode);
            if (product != null)
            {
                ProductScanned?.Invoke(product);
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid product code. SCANNER!!");
            }
        }
    }
}
