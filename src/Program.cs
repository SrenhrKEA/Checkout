using BusinessLogic;
using Services;
using System.Text;

class Program
{
    static async Task Main(string[] args)
    {
        var productCatalog = new ProductCatalog();
        var initializer = new ProductCatalogInitializer();
        initializer.PopulateProductCatalog(productCatalog);

        var scanner = new Scanner(productCatalog);

        var expensiveCalculator = new ExpensivePriceCalculator();
        var cheapCalculator = new CheapPriceCalculator();

        var checkoutManager = new CheckoutManager(cheapCalculator, expensiveCalculator);

        scanner.ProductScanned += (product) =>
        {
            checkoutManager.AddScannedProduct(product);
            UpdateDisplayPrice(checkoutManager);
        };

        // Main loop
        do
        {
            Console.WriteLine("Please scan a product (enter product code), press 'Tab' to view scanned products, or 'ESC' to quit:");
            var keyInfo = Console.ReadKey(intercept: true);

            if (keyInfo.Key == ConsoleKey.Escape)
            {
                if (HandleExitConfirmation())
                    return;
            }
            else if (keyInfo.Key == ConsoleKey.Tab)
            {
                HandleScannedProducts(checkoutManager);
            }
            else
            {
                // Process regular key presses for product scanning
                char productCode = char.ToUpper(keyInfo.KeyChar);
                if (IsValidProductCode(productCode))
                {
                    await HandleProductScan(productCode, scanner);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid product code.");
                }
            }
        } while (true);

    }


    private static async Task HandleProductScan(char productCode, Scanner scanner)
    {
        if (IsValidProductCode(productCode))
        {
            await scanner.ScanAsync(productCode);
            Console.WriteLine($"Product {productCode} scanned.");
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid product code.");
        }
    }


    private static void HandleScannedProducts(CheckoutManager checkoutManager)
    {
        do
        {
            Console.WriteLine("Scanned Products:");

            DisplayScannedProducts(checkoutManager);
            Console.WriteLine("Enter the index of the product to remove, 'Tab' for the receipt, or press ESC to exit:");


            string input = "";
            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine("Continuing...");
                    return;
                }
                else if (keyInfo.Key == ConsoleKey.Tab)
                {
                    HandleReceipt(checkoutManager);
                    return;
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && input.Length > 0)
                {
                    input = input[0..^1];  // Remove last character for backspace
                    Console.Write("\b \b");  // Reflect backspace in console
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    input += keyInfo.KeyChar;  // Build the input string
                    Console.Write(keyInfo.KeyChar + "\n");  // Echo the character

                }
            }
            while (true);

            if (int.TryParse(input, out int index) && index >= 1)
            {
                if (checkoutManager.RemoveScannedProductAt(index - 1))
                {
                    Console.WriteLine("Product removed successfully.");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid index. No product removed.");
                }
            }
            else if (!string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Invalid input. Please enter a valid index.");
            }

        } while (true);
    }

    private static bool HandleExitConfirmation()
    {
        Console.WriteLine("Are you sure you want to exit? Press ESC again to quit.");

        var confirmKey = Console.ReadKey(intercept: true);
        Console.Clear();

        if (confirmKey.Key == ConsoleKey.Escape)
            return confirmKey.Key == ConsoleKey.Escape;

        else
        {
            Console.WriteLine("Continuing...");
            return false;
        }

    }

    private static void HandleReceipt(CheckoutManager checkoutManager)
    {
        string receipt = checkoutManager.CompleteCheckout();
        Console.WriteLine(receipt);
        checkoutManager.EmptyBin();
    }

    private static bool IsValidProductCode(char code)
    {
        return char.IsLetter(code);
    }

    private static void UpdateDisplayPrice(CheckoutManager checkoutManager)
    {
        string totalPrice = checkoutManager.GetTotalPrice();
        Console.WriteLine($"Number of items: {checkoutManager.ItemCount} || Total Price: {totalPrice}");
    }

    private static void DisplayScannedProducts(CheckoutManager checkoutManager)
    {
        var scannedProducts = checkoutManager.GetScannedProducts();
        int index = 1;

        foreach (var product in scannedProducts)
        {
            var message = new StringBuilder($"#{index++}, Product Code: {product.Code}");

            if (product.IsMultipack)
            {
                message.Append($", Multipack: {product.MultipackQuantity}");
            }

            if (product.IsCampaignProduct)
            {
                message.Append($", Campaign: {product.CampaignDescription}");
            }

            Console.WriteLine(message);
        }
    }

}
