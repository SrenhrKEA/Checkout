using Models;
using System.Text;

namespace BusinessLogic
{
    public class ExpensivePriceCalculator : PriceCalculator
    {
        public override string CalculateTotalPrice(List<Product> products)
        {
            var groupedProducts = products
                .GroupBy(p => p.Group)
                .OrderBy(g => g.Key);

            decimal totalPrice = 0;
            var receiptBuilder = new StringBuilder();
            receiptBuilder.AppendLine($"Receipt:");
            foreach (var group in groupedProducts)
            {
                receiptBuilder.AppendLine($"Product Group: {group.Key}");

                foreach (var productGroup in group.GroupBy(p => p.Code).OrderBy(p => p.Key))
                {
                    var product = productGroup.First();
                    int quantity = productGroup.Count();

                    decimal productPrice = product.Price;
                    decimal priceForProductGroup;

                    if (product.IsMultipack)
                    {
                        productPrice *= product.MultipackQuantity;
                    }

                    if (product.IsCampaignProduct && quantity >= product.CampaignQuantity)
                    {
                        int discountableUnits = quantity / product.CampaignQuantity * product.CampaignQuantity;
                        int nonDiscountableUnits = quantity - discountableUnits;

                        decimal discount = productPrice * discountableUnits * (product.CampaignDiscount / 100m);
                        decimal discountedPrice = productPrice * discountableUnits - discount;
                        decimal nonDiscountedPrice = productPrice * nonDiscountableUnits;

                        priceForProductGroup = discountedPrice + nonDiscountedPrice;
                    }
                    else if (!product.IsMultipack)
                    {
                        priceForProductGroup = productPrice * quantity;
                    }
                    else
                    {
                        priceForProductGroup = productPrice;
                    }

                    totalPrice += priceForProductGroup;
                    if (product.IsMultipack)
                        receiptBuilder.Append($"  Product: {product.MultipackBaseProductCode} Multipack: {product.MultipackQuantity}");
                    else
                        receiptBuilder.Append($"  Product: {product.Code}");
                    receiptBuilder.AppendLine($", Quantity: {quantity}, Price: {priceForProductGroup.ToString("C2")}");

                }
            }

            receiptBuilder.Append($"Total Price: {totalPrice.ToString("C2")}");
            return receiptBuilder.ToString();
        }
    }
}
