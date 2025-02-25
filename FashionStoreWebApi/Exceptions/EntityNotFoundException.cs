namespace FashionStoreWebApi.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message) : base(message)
        {
        }

        public EntityNotFoundException(string entityName, object id)
            : base($"The {entityName} with ID '{id}' was not found.")
        {
        }
    }
}
