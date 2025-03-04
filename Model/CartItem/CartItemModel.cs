using Strunchik.Model.Basket;
using Strunchik.Model.Item;
using System.ComponentModel.DataAnnotations;

namespace Strunchik.Model.CartItem;

public class CartItemModel
{
    [Key]
    public int CartItemId { get; set; }
    public int BasketId { get; set; }
    public BasketModel Basket { get; set; }

    public int ItemId { get; set; }
    public ItemModel Item { get; set; }

    public int Quantity { get; set; } = 1;
}
