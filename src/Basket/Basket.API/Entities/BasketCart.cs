using System.Collections.Generic;
using System.Linq;

namespace Basket.API.Entities
{
    public class BasketCart
    {
        public string Username { get; set; }
        public List<BasketCartItem> Items { get; set; } = new();

        public BasketCart()
        {
        }

        public BasketCart(string username)
        {
            Username = username;
        }

        public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);
    }
}