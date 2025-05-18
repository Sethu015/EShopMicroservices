
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data
{
    public class CachedBasketRepository(IBasketRepository repository,IDistributedCache cache) : IBasketRepository
    {
        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
        {
            var cachedBasket = await cache.GetStringAsync(userName, cancellationToken);
            if (!string.IsNullOrWhiteSpace(cachedBasket))
                return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket);
            var basket = await repository.GetBasket(userName, cancellationToken);
            await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);
            return basket;
        }

        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
        {
            var result = await repository.DeleteBasket(userName, cancellationToken);
            await cache.RemoveAsync(userName,cancellationToken);
            return result;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart shopping, CancellationToken cancellation = default)
        {
            var basket = await repository.StoreBasket(shopping, cancellation);
            await cache.SetStringAsync(shopping.UserName, JsonSerializer.Serialize(shopping), cancellation);
            return basket;
        }
    }
}
