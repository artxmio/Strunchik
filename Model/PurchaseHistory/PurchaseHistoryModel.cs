using Strunchik.Model.User;
using System.ComponentModel.DataAnnotations;

namespace Strunchik.Model.PurchaseHistory;

public class PurchaseHistoryModel
{
    [Key]
    public int PurchaseId { get; set; }
    public int UserId { get; set; }
    public UserModel User { get; set; }

    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public DateTime PurchaseDate { get; set; } = DateTime.Now;
}
