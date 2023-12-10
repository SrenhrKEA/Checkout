using BusinessLogic;
using Services;

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

        scanner.ProductScanned += checkoutManager.AddScannedProduct;

        DisplayWelcomeMessage();

        do
        {
            var keyInfo = Console.ReadKey(intercept: true);
            Console.Clear();
            DisplayWelcomeMessage();

            switch (keyInfo.Key)
            {
                case ConsoleKey.Escape:
                    if (await HandleExitConfirmation())
                        return;
                    break;
                case ConsoleKey.OemMinus:
                    HandleRemoveProduct(checkoutManager);
                    break;
                case ConsoleKey.Tab:
                    HandleReceipt(checkoutManager);
                    break;
                default:
                    await HandleProductScan(keyInfo, scanner);
                    break;
            }
        } while (true);
    }

    private static async Task HandleProductScan(ConsoleKeyInfo keyInfo, Scanner scanner)
    {
        char productCode = char.ToUpper(keyInfo.KeyChar);
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

    private static void HandleRemoveProduct(CheckoutManager checkoutManager)
    {
        do
        {
            Console.Clear();
            Console.WriteLine("Scanned Products:");
            checkoutManager.DisplayScannedProducts();
            Console.WriteLine("Enter the index of the product to remove:");

            if (int.TryParse(Console.ReadLine(), out int index) && index >= 0)
            {
                if (checkoutManager.RemoveScannedProductAt(index))
                {
                    Console.WriteLine("Product removed successfully.");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid index. No product removed.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid index.");
            }
        } while (true);
    }

    private static async Task<bool> HandleExitConfirmation()
    {
        Console.WriteLine("Are you sure you want to exit? Press ESC again to quit.");
        var confirmKey = Console.ReadKey(intercept: true);

        if (confirmKey.Key != ConsoleKey.Escape)
        {
            Console.WriteLine("Continuing...");
            await Task.Delay(500);
        }

        return confirmKey.Key == ConsoleKey.Escape;
    }

    private static void HandleReceipt(CheckoutManager checkoutManager)
    {
        // Console.WriteLine("Scanned Products:");
        // checkoutManager.DisplayScannedProducts();
        // Console.WriteLine("Enter the index of the product to remove:");

        // if (int.TryParse(Console.ReadLine(), out int index) && index >= 0)
        // {
        //     if (checkoutManager.RemoveScannedProductAt(index))
        //     {
        //         Console.WriteLine("Product removed successfully.");
        //     }
        //     else
        //     {
        //         Console.WriteLine("Invalid index. No product removed.");
        //     }
        // }
        // else
        // {
        //     Console.WriteLine("Invalid input. Please enter a valid index.");
        // }
    }

    private static bool IsValidProductCode(char code)
    {
        return char.IsLetter(code);
    }

    private static void DisplayWelcomeMessage()
    {
        Console.WriteLine("Please scan a product (enter product code), press '-' to remove a product, or ESC to quit:");
    }
}
