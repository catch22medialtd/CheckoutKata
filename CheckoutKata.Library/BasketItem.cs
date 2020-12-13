namespace CheckoutKata.Library
{
    public class BasketItem
    {
        public string SKU { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public bool IsMultiBuyItem { get; set; }
    }
}