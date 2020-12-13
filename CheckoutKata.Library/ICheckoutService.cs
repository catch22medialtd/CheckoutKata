namespace CheckoutKata.Library
{
    public interface ICheckoutService
    {
        decimal GetTotalPrice();
        void Scan(string item);
    }
}