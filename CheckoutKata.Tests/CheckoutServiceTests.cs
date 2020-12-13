using CheckoutKata.Library;
using NUnit.Framework;
using System.Collections.Generic;

namespace CheckoutKata.Tests
{
    [TestFixture]
    public class Tests
    {
        private ICheckoutService _checkoutService;

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
        }

        [Test]
        public void Given_Basket_Is_Empty_When_GetTotalPrice_Invoked_Then_Zero_Returned()
        {
            var actual = _checkoutService.GetTotalPrice();
            var expected = 0;

            Assert.AreEqual(expected, actual);

        }

        [Test]
        public void Given_Basket_Has_One_Item_When_GetTotalPrice_Invoked_Then_Correct_Total_Returned()
        {
            var items = new string[] { "A" };

            foreach (var item in items)
            {
                _checkoutService.Scan(item);
            }

            var actual = _checkoutService.GetTotalPrice();
            var expected = 50;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Given_Basket_Has_Multiple_Non_MultiBuy_Items_When_GetTotalPrice_Is_Invoked_Then_Correct_Total_Returned()
        {
            var items = new string[] { "A", "B", "C" };

            foreach (var item in items)
                _checkoutService.Scan(item);

            var actual = _checkoutService.GetTotalPrice();
            var expected = 100;

            Assert.AreEqual(actual, expected);
        }

        [Test]
        public void Given_Basket_Has_MultiBuy_Items_When_GetTotalPrice_Is_Invoked_Then_Correct_Total_Returned()
        {
            var items = new string[] { "A", "A", "A", "B" };

            foreach (var item in items)
                _checkoutService.Scan(item);

            var actual = _checkoutService.GetTotalPrice();
            var expected = 160;

            Assert.AreEqual(actual, expected);
        }

        [TestCase(new string[] { }, 0)]
        [TestCase(new string[] { "A" }, 50)]
        [TestCase(new string[] { "A", "B" }, 80)]
        [TestCase(new string[] { "A", "A", "A", "B" }, 160)]
        [TestCase(new string[] { "A", "A", "A", "B", "B", "D" }, 190)]
        [TestCase(new string[] { "A", "A", "A", "B", "B", "C", "D" }, 210)]

        public void Given_Basket_Has_Various_Different_Items_When_GetTotalPrice_Is_Invoked_Then_Correct_Total_Is_Returned(string[] items, decimal expected)
        {
            foreach (var item in items)
                _checkoutService.Scan(item);

            var actual = _checkoutService.GetTotalPrice();

            Assert.AreEqual(actual, expected);
        }
    }
}