using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache redisCache)
        {
            this._redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

        public async Task<ShoppingCart> GetShoppingCart(string UserName)
        {
            var basket = await _redisCache.GetStringAsync(UserName);

            if (String.IsNullOrEmpty(basket))
                return null;

            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateShoppingCart(ShoppingCart basket)
        {
            await _redisCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));

            return await GetShoppingCart(basket.UserName);
        }

        public async Task DeleteShoppingCart(string UserName)
        {
            var basket = await _redisCache.GetStringAsync(UserName);

            if (!String.IsNullOrEmpty(basket))
            {
                await _redisCache.RemoveAsync(UserName);
            }
        }
    }
}
