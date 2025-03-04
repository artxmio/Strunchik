using Strunchik.Model.Basket;
using Strunchik.Model.CartItem;
using System.ComponentModel.DataAnnotations;

namespace Strunchik.Model.Item;

public class ItemModel
{
    [Key]
    public int ItemId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public ItemsType Type { get; set; }

    public ICollection<CartItemModel> CartItems { get; set; }
}