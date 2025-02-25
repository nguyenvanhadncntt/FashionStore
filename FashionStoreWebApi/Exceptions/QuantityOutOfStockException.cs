namespace FashionStoreWebApi.Exceptions
{
    public class QuantityOutOfStockException : Exception
    {
        public QuantityOutOfStockException(string message) : base(message) { }
    }
}
