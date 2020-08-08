using ShoppingCart.Bll.Models;
using System.Collections.Generic;

namespace ShoppingCart.Bll.Services
{
    public interface IShoppingCartService
    {
        decimal CalculateShoppingCart(IEnumerable<Plu> cartItems);
    }
}
