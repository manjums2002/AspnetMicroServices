using Basket.API.Entities;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetShoppingCart(string UserName);

        Task<ShoppingCart> UpdateShoppingCart(ShoppingCart cart);

        Task DeleteShoppingCart(string UserName);
    }
}
