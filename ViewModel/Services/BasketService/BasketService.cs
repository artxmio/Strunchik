using Microsoft.EntityFrameworkCore;
using Strunchik.Model.CartItem;

namespace Strunchik.ViewModel.Services.BasketService;

public class BasketService
{
    private readonly ApplicationContext.ApplicationContext _context;

    public BasketService(ApplicationContext.ApplicationContext context)
    {
        _context = context;
    }

    public async Task AddItemToBasket(int userId, int itemId, int quantity)
    {
        var user = _context.Users.Include(u => u.Basket).ThenInclude(b => b.CartItems).FirstOrDefault(u => u.UserId == userId);

        if (user != null && user.Basket != null)
        {
            var basket = user.Basket;
            var item = _context.Items.FirstOrDefault(i => i.ItemId == itemId);

            if (item != null)
            {
                var existingCartItem = basket.CartItems
                    .FirstOrDefault(ci => ci.ItemId == itemId);

                if (existingCartItem != null)
                {
                    existingCartItem.Quantity += quantity;
                }
                else
                {
                    var cartItem = new CartItemModel
                    {
                        BasketId = basket.BasketId,
                        ItemId = item.ItemId,
                        Item = item,
                        Quantity = quantity
                    };
                    basket.CartItems.Add(cartItem);
                }

                _context.SaveChanges();
            }
        }
    }

    public async Task DeleteItemFromBasket(object _)
    {
        if (_ != null && _ is CartItemModel itemToRemove)
        {
            var cartItem = _context.CartItems
                .FirstOrDefault(ci => ci.CartItemId == itemToRemove.CartItemId);

            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}
