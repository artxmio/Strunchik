using Strunchik.Model.Basket;
using System.ComponentModel.DataAnnotations;

namespace Strunchik.Model.User;

public class UserModel
{
    [Key]
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; } = "";

    public BasketModel Basket { get; set; }
}
