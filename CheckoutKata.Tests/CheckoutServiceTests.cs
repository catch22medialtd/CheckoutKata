using CheckoutKata.Library;
using NUnit.Framework;
using System.Collections.Generic;

namespace CheckoutKata.Tests
{
    [TestFixture]
    public class Tests
    {
        private ICheckoutService _checkoutService;
        private IShoppingBagService _shoppingBagService;

        [SetUp]
        public void Setup()
        {
            var pricingRules = new List<PricingRule>()
            {
                new PricingRule() { SKU = "A", UnitPrice = 50, MultiBuyPrice = 130, MultiBuyQuantity = 3 },
                new PricingRule() { SKU = "B", UnitPrice = 30, MultiBuyPrice = 45, MultiBuyQuantity = 2 },
                new PricingRule() { SKU = "C", UnitPrice = 20 },
                new PricingRule() { SKU = "D", UnitPrice = 15 }
            };

            _checkoutService = new CheckoutService(pricingRules);
            _shoppingBagService = new ShoppingBagService();
        }

        [TestCase(new string[] { }, 0)]
        [TestCase(new string[] { "A" }, 55)]
        [TestCase(new string[] { "A", "B" }, 85)]
        [TestCase(new string[] { "A", "A", "A", "B" }, 165)]
        [TestCase(new string[] { "A", "A", "A", "B", "B", "D" }, 200)]
        [TestCase(new string[] { "A", "A", "A", "B", "B", "C", "D" }, 220)]
        [TestCase(new string[] { "A", "A", "A", "A", "A", "A", "B", "B", "C", "D" }, 350)]
        public void Given_Basket_Has_Various_Different_Items_When_GetTotalPrice_Is_Invoked_Then_Correct_Total_Is_Returned_Including_ShoppingBag_Cost(string[] items, decimal expected)
        {
            foreach (var item in items)
                _checkoutService.Scan(item);

            var actual = _checkoutService.GetTotalPrice();

            Assert.AreEqual(actual, expected);
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(3, 1)]
        [TestCase(5, 1)]
        [TestCase(6, 2)]
        [TestCase(10, 2)]
        [TestCase(11, 3)]
        [TestCase(20, 4)]
        [TestCase(23, 5)]
        public void Given_Basket_Has_X_Items_When_GetNoOfShoppingBags_Is_Invoked_Then_Correct_Bag_Total_Is_Returned(int basketItems, int expected)
        {
            var actual = _shoppingBagService.GetTotalShoppingBags(basketItems);

            Assert.AreEqual(actual, expected);
        }
    }
}