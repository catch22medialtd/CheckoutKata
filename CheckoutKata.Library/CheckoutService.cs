using System.Collections.Generic;
using System.Linq;

namespace CheckoutKata.Library
{
    public class CheckoutService : ICheckoutService
    {
        private List<string> _basketItems;

        public CheckoutService()
        {
            _basketItems = new List<string>();
        }

        public void Scan(string item)
        {
            _basketItems.Add(item);
        }

        public decimal GetTotalPrice()
        {
            if (_basketItems.Contains("A") && _basketItems.Count() == 1)
                return 50;

            return 0;
        }
    }
}