using CheckoutKata.Library;
using NUnit.Framework;

namespace CheckoutKata.Tests
{
    [TestFixture]
    public class Tests
    {
        private ICheckoutService _checkoutService;

        [SetUp]
        public void Setup()
        {
            _checkoutService = new CheckoutService();
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
    }
}