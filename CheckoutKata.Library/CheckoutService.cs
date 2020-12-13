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
            var pricingRule = _pricingRules.FirstOrDefault(pr => pr.SKU == item);
            var basketItem = new BasketItem() { SKU = pricingRule.SKU, Total = pricingRule.UnitPrice };

            _basketItems.Add(basketItem);
        }

        public decimal GetTotalPrice()
        {
            if (!_basketItems.Any())
                return 0;

            return _basketItems.Sum(bi => bi.Total);
        }
    }
}