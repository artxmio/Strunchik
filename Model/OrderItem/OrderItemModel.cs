using Strunchik.Model.Item;
using Strunchik.Model.Order;
using System.ComponentModel.DataAnnotations;

namespace Strunchik.Model.OrderItem;

public class OrderItemModel
{
    [Key]
    public int OrderItemId { get; set; }
    public int OrderId { get; set; }
    public OrderModel Order { get; set; }

    public int ItemId { get; set; }
    public ItemModel Item { get; set; }

    public int Quantity { get; set; } = 1;
    public decimal Price { get; set; }
}