using Strunchik.Model.OrderItem;
using Strunchik.Model.PurchaseHistory;

namespace Strunchik.ViewModel.Services.PurchaseHistoryService;

public class PurchaseHistoryService(ApplicationContext.ApplicationContext context)
{
    private ApplicationContext.ApplicationContext _context = context;

    public void AddOrderToPurchaseHistory(int userId, List<OrderItemModel> orderItems)
    {
        foreach (var orderItem in orderItems)
        {
            var purchaseHistory = new PurchaseHistoryModel
            {
                UserId = userId,
                ProductName = orderItem.Item.Title,
                Quantity = orderItem.Quantity,
                Price = orderItem.Price,
                PurchaseDate = DateTime.Now
            };

            _context.PurchaseHistory.Add(purchaseHistory);
        }

        _context.SaveChanges();
    }
}
