using Strunchik.Model.Item;
using Strunchik.Model.User;
using System.ComponentModel.DataAnnotations;

namespace Strunchik.Model.Basket;

public class BasketModel
{
    [Key]
    public int BasketId { get; set; }
    public List<ItemModel> Items { get; set; } = new List<ItemModel>();
    public int Price { get; set; }

    public int UserId { get; set; }
    public UserModel User { get; set; }
}
