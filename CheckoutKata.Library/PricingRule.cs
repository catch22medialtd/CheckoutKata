namespace CheckoutKata.Library
{
    public class PricingRule
    {
        public string SKU { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal MultiBuyPrice { get; set; }
        public int MultiBuyQuantity { get; set; }
    }
}