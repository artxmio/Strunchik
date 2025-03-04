using Strunchik.Model.CartItem;
using Strunchik.Model.Item;
using Strunchik.Model.User;
using System.ComponentModel.DataAnnotations;

namespace Strunchik.Model.Basket;

public class BasketModel
{
    [Key]
    public int BasketId { get; set; }

    public int UserId { get; set; }
    public UserModel User { get; set; }

    public ICollection<CartItemModel> CartItems { get; set; }
}
