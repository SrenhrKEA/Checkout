using Models;
namespace Services
{
    public class ProductCatalogInitializer
    {
        public static void PopulateProductCatalog(ProductCatalog productCatalog)
        {
            // Regular products
            productCatalog.AddProduct(new Product('A', 50m, 1));
            productCatalog.AddProduct(new Product('B', 30m, 1));
            productCatalog.AddProduct(new Product('C', 20m, 2));
            productCatalog.AddProduct(new Product('D', 15m, 2));
            productCatalog.AddProduct(new Product('E', 60m, 3));
            productCatalog.AddProduct(new Product('F', 45m, 3));
            productCatalog.AddProduct(new Product('G', 100m, 4));
            productCatalog.AddProduct(new Product('H', 120m, 4));
            productCatalog.AddProduct(new Product('I', 85m, 5));
            productCatalog.AddProduct(new Product('J', 75m, 5));
            productCatalog.AddProduct(new Product('K', 55m, 6));
            productCatalog.AddProduct(new Product('L', 95m, 6));
            productCatalog.AddProduct(new Product('M', 65m, 7));
            productCatalog.AddProduct(new Product('N', 35m, 7));
            productCatalog.AddProduct(new Product('O', 40m, 8));
            productCatalog.AddProduct(new Product('P', 0, 0)); // bottle deposit
            productCatalog.AddProduct(new Product('Q', 25m, 9));
            productCatalog.AddProduct(new Product('R', 80m, 9));
            productCatalog.AddProduct(new Product('S', 70m, 1));
            productCatalog.AddProduct(new Product('T', 20m, 2));
            productCatalog.AddProduct(new Product('U', 110m, 3));
            productCatalog.AddProduct(new Product('V', 130m, 4));
            productCatalog.AddProduct(new Product('W', 140m, 5));
            productCatalog.AddProduct(new Product('X', 150m, 6));
            productCatalog.AddProduct(new Product('Y', 160m, 7));
            productCatalog.AddProduct(new Product('Z', 170m, 8));

            // Multipack products
            productCatalog.AddProduct(new Product('E', 50m, 3, isMultipack: true, multipackBaseProductCode: 'A', 6));
            productCatalog.AddProduct(new Product('F', 30m, 3, isMultipack: true, multipackBaseProductCode: 'B', 10));

            // Campaign products
            productCatalog.AddProduct(new Product('G', 100m, 4, isCampaignProduct: true, campaignDiscount: 10, campaignQuantity: 2, campaignDescription: "Buy 2 at 10% discount"));
            productCatalog.AddProduct(new Product('H', 120m, 4, isCampaignProduct: true, campaignDiscount: 20, campaignQuantity: 3, campaignDescription: "Buy 3 at 20% discount"));

            //Multipack AND Campaign products
            productCatalog.AddProduct(new Product('I', 20m, 5, isMultipack: true, multipackBaseProductCode: 'C', 6, isCampaignProduct: true, campaignDiscount: 10, campaignQuantity: 2, campaignDescription: "Buy 2 at 10% discount"));
            productCatalog.AddProduct(new Product('J', 15m, 5, isMultipack: true, multipackBaseProductCode: 'D', 3, isCampaignProduct: true, campaignDiscount: 20, campaignQuantity: 3, campaignDescription: "Buy 3 at 20% discount"));
        }
    }

}