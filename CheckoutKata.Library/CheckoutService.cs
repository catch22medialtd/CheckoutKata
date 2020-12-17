using System.Collections.Generic;
using System.Linq;

namespace CheckoutKata.Library
{
    public class CheckoutService : ICheckoutService
    {
        private IShoppingBagService _shoppingBagService;
        private List<PricingRule> _pricingRules;
        private readonly int _shoppingBagCost;
        private List<BasketItem> _basketItems;

        public CheckoutService(List<PricingRule> pricingRules, int shoppingBagCost = 5)
        {
            _shoppingBagService = new ShoppingBagService();
            _pricingRules = pricingRules;
            _shoppingBagCost = shoppingBagCost;
            _basketItems = new List<BasketItem>();
        }

        public void Scan(string item)
        {
            var pricingRule = _pricingRules.FirstOrDefault(pr => pr.SKU == item);
            
            var basketItem = new BasketItem()
            {
                SKU = pricingRule.SKU,
                Quantity = 1,
                Total = pricingRule.UnitPrice
            };

            _basketItems.Add(basketItem);

            if (pricingRule.MultiBuyQuantity > 0)
                CheckForMultiBuyDiscounts(pricingRule);
        }

        private void CheckForMultiBuyDiscounts(PricingRule pricingRule)
        {
            if (_basketItems.Count(bi => bi.SKU == pricingRule.SKU && !bi.IsMultiBuyItem) == pricingRule.MultiBuyQuantity)
            {
                _basketItems.RemoveAll(bi => bi.SKU == pricingRule.SKU && !bi.IsMultiBuyItem);

                var multiBuyBasketItem = new BasketItem()
                {
                    SKU = pricingRule.SKU,
                    Quantity = pricingRule.MultiBuyQuantity,
                    Total = pricingRule.MultiBuyPrice,
                    IsMultiBuyItem = true
                };

                _basketItems.Add(multiBuyBasketItem);
            }
        }

        public decimal GetTotalPrice()
        {
            if (!_basketItems.Any())
                return 0;

            var total = _basketItems.Sum(bi => bi.Total);
            var totalShoppingBags = _shoppingBagService.GetTotalShoppingBags(GetBasketCount());

            return total + _shoppingBagCost * totalShoppingBags;
        }

        private int GetBasketCount()
        {
            int count = 0;

            foreach (var item in _basketItems)
                count += item.IsMultiBuyItem ? item.Quantity : 1;

            return count;
        }
    }
}