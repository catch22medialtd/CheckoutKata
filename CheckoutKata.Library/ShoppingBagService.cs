namespace CheckoutKata.Library
{
    public class ShoppingBagService : IShoppingBagService
    {
        public int GetTotalShoppingBags(int shoppingBasketCount)
        {
            const int maxBagCapacity = 5;

            int fullBags = shoppingBasketCount / maxBagCapacity;
            int halfBags = shoppingBasketCount % maxBagCapacity > 0 ? 1 : 0;

            return fullBags + halfBags;
        }
    }
}