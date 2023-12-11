using Models;

namespace BusinessLogic
{
    public abstract class PriceCalculator
    {
        public abstract string CalculateTotalPrice(List<Product> products);
    }
}
