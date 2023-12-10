using Models;

namespace BusinessLogic
{
    public abstract class PriceCalculator
    {
        public abstract decimal CalculateTotalPrice(List<Product> products);
    }
}
