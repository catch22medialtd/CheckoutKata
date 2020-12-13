using System.Collections.Generic;
using System.Linq;

namespace CheckoutKata.Library
{
    public class CheckoutService : ICheckoutService
    {
        private List<PricingRule> _pricingRules;
        private List<BasketItem> _basketItems;

        public CheckoutService(List<PricingRule> pricingRules)
        {
            _pricingRules = pricingRules;
            _basketItems = new List<BasketItem>();
        }

        public void Scan(string item)
        {
            // get the pricing rule for this item
            var pricingRule = _pricingRules.FirstOrDefault(pr => pr.SKU == item);
            
            // create a new basketitem for this using these rules
            var basketItem = new BasketItem() { SKU = pricingRule.SKU, Quantity = 1, Total = pricingRule.UnitPrice };

            // add the item to the basket
            _basketItems.Add(basketItem);

            // Its a multibuy product check for discounts in the basket
            if (pricingRule.MultiBuyQuantity > 0)
                CheckForMultiBuyDiscounts(pricingRule);
        }

        private void CheckForMultiBuyDiscounts(PricingRule pricingRule)
        {
            // If we have enough single items in the basket to satisfy a multibuy rule
            if (_basketItems.Count(ci => ci.SKU == pricingRule.SKU && ci.IsMultiBuyItem == false) == pricingRule.MultiBuyQuantity)
            {
                // remove the single items
                _basketItems.RemoveAll(ci => ci.SKU == pricingRule.SKU && !ci.IsMultiBuyItem);

                // add a multibuy item in there place
                var multiBuyBasketItem = new BasketItem() { SKU = pricingRule.SKU, Quantity = pricingRule.MultiBuyQuantity, Total = pricingRule.MultiBuyPrice, IsMultiBuyItem = true };

                _basketItems.Add(multiBuyBasketItem);
            }
        }

        public decimal GetTotalPrice()
        {
            if (!_basketItems.Any())
                return 0;

            return _basketItems.Sum(bi => bi.Total);
        }
    }
}