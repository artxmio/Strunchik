using Microsoft.EntityFrameworkCore;
using Strunchik.Model.CartItem;
using System.Collections.ObjectModel;

namespace Strunchik.ViewModel.Services.BasketService;

public class BasketService
{
    private readonly ApplicationContext.ApplicationContext _context;

    public BasketService(ApplicationContext.ApplicationContext context)
    {
        _context = context;
    }

    public void AddItemToBasket(int userId, int itemId, int quantity)
    {
        var user = _context.Users.Include(u => u.Basket).ThenInclude(b => b.CartItems).FirstOrDefault(u => u.UserId == userId);

        if (user != null && user.Basket != null)
        {
            var basket = user.Basket;
            var item = _context.Items.FirstOrDefault(i => i.ItemId == itemId);

            if (item != null)
            {
                var cartItem = new CartItemModel
                {
                    BasketId = basket.BasketId,
                    ItemId = item.ItemId,
                    Item = item,
                    Quantity = quantity
                };
                basket.CartItems.Add(cartItem);
                _context.SaveChanges();
            }
        }
    }
}
