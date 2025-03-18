using Strunchik.Model.OrderItem;
using Strunchik.Model.User;
using System.ComponentModel.DataAnnotations;

namespace Strunchik.Model.Order;

public class OrderModel
{
    [Key]
    public int OrderId { get; set; }
    public int UserId { get; set; }
    public UserModel User { get; set; }

    public DateTime OrderDate { get; set; } = DateTime.Now;
    public decimal TotalAmount { get; set; }

    public ICollection<OrderItemModel> OrderItems { get; set; }
}