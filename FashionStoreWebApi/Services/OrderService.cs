using FashionStoreWebApi.Data;

namespace FashionStoreWebApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly FashionStoreDbContext _dbContext;

        public OrderService(FashionStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //public placeOrder()
    }
}
