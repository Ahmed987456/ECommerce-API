using E_Commerce_API.Dtos.OrdersDto;

namespace E_Commerce_API.Services.OrderService
{
    public interface IOrderService
    {
        Task<(Order? order, string? error)> CreateOrder(int userId);

        Task<List<UserOrdersDto>> GetAllUserOrders(int UserId);

        Task<OrderDetailsDto?> GetOrderDetails(int id);

        Task UpdateStatus();

        Task<Order?> GetOrderById(int OrderId);

        Task<string> CancelOrder(int orderId);

        Task<bool> HasOrders(int UserId);

        Task<IEnumerable<AdminOrderDto>> GetAllOrders();
    }
}
