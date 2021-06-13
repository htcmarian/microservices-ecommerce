using System.Threading.Tasks;
using Basket.API.Entities;

namespace Basket.API.Repositories
{
    public interface IBasketRepository
    {
        Task<BasketCart> GetBasket(string username);
        Task<BasketCart> UpdateBasket(BasketCart basket);
        Task DeleteBasket(string username);
    }
}