namespace FashionStoreWebApi.Exceptions
{
    public class EntityAlreadyExistingException : Exception
    {
        public EntityAlreadyExistingException(string message) : base(message) { }
    }
}
