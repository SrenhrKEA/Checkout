using Models;

namespace BusinessLogic
{
    public class CheapPriceCalculator : PriceCalculator
    {
        public override string CalculateTotalPrice(List<Product> products)
        {
            decimal totalPrice = 0;

            var groupedProducts = products.GroupBy(p => p.Code);

            foreach (var group in groupedProducts)
            {
                var product = group.First();
                int quantity = group.Count();

                decimal productPrice = product.Price;

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

                    productPrice = discountedPrice + nonDiscountedPrice;
                }
                else if (!product.IsMultipack)
                {
                    productPrice *= quantity;
                }

                totalPrice += productPrice;
            }

            return totalPrice.ToString("C2");
        }
    }
}
