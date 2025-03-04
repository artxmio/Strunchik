using Strunchik.Model.Basket;
using System.ComponentModel.DataAnnotations;

namespace Strunchik.Model.Item;

public class ItemModel
{
    [Key]
    public int ItemId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public ItemsType Type { get; set; }

    public int BasketId { get; set; }
    public BasketModel Basket { get; set; }
}