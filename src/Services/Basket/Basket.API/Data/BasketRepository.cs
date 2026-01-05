
namespace Basket.API.Data
{
    public class BasketRepository(IDocumentSession session) : IBasketRepository
    {
        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
        {
            var result = await session.LoadAsync<ShoppingCart>(userName,cancellationToken);
            return result is not null ? result : throw new BasketNotFoundException(userName);
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart shoppingCart, CancellationToken cancellationToken = default)
        {
            session.Store(shoppingCart);
            return shoppingCart;
        }
        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
        {
            session.Delete<ShoppingCart>(userName);
            return true;
        }
    }
}
