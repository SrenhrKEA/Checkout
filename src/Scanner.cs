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

        public async Task<bool> ScanAsync(char productCode)
        {
            await Task.Delay(500);

            try
            {
                Product product = _productCatalog.GetProduct(productCode);
                ProductScanned?.Invoke(product);
                return true;
            }
            catch (KeyNotFoundException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
