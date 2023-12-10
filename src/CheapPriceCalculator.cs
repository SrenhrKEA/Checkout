using Models;

namespace BusinessLogic
{
    public class CheapPriceCalculator : PriceCalculator
    {
        public override decimal CalculateTotalPrice(List<Product> products)
        {
            decimal totalPrice = 0;
            // Implement simple total price calculation
            foreach (Product product in products)
            {
                if (product.IsMultipack)
                {
                    // Calculate multipack price
                    totalPrice += product.Price*product.MultipackQuantity;
                }

                if (product.IsCampaignProduct)
                {
                    // Apply campaign discount or offer
                    totalPrice += product.Price;
                }

                // If a product is both, you might need additional logic
                if (product.IsMultipack && product.IsCampaignProduct)
                {
                    // Special handling for combined multipack and campaign pricing
                    totalPrice += product.Price;
                }
                else
                {
                    // Default case: add the regular product price
                    totalPrice += product.Price;
                }
            }
            return totalPrice;
        }
    }
}
