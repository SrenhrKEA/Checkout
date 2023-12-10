using Models;

namespace BusinessLogic
{
    public class CheapPriceCalculator : PriceCalculator
    {
        public override decimal CalculateTotalPrice(List<Product> products)
        {
            decimal totalPrice = 0;

            // Group products by their code to handle quantities.
            var groupedProducts = products.GroupBy(p => p.Code);

            foreach (var group in groupedProducts)
            {
                var product = group.First(); // All products in the group are identical.
                int quantity = group.Count();

                decimal productPrice = product.Price;

                // Calculate price for multipack products
                if (product.IsMultipack)
                {
                    productPrice *= product.MultipackQuantity;
                }

                if (product.IsCampaignProduct && quantity >= product.CampaignQuantity)
                {
                    int discountableUnits = quantity / product.CampaignQuantity * product.CampaignQuantity;
                    int nonDiscountableUnits = quantity - discountableUnits;

                    // Calculate price for units with discount
                    decimal discount = productPrice * discountableUnits * (product.CampaignDiscount / 100m);
                    decimal discountedPrice = productPrice * discountableUnits - discount;

                    // Calculate price for units without discount
                    decimal nonDiscountedPrice = productPrice * nonDiscountableUnits;

                    productPrice = discountedPrice + nonDiscountedPrice;
                }
                else if (!product.IsMultipack)
                {
                    // Regular pricing for products not in campaign and not multipack.
                    productPrice *= quantity;
                }

                totalPrice += productPrice;
            }

            return totalPrice;
        }
    }
}
