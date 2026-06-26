using E_Commerce_API.Dtos.OrdersDto;
using E_Commerce_API.Enums;

namespace E_Commerce_API.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> CancelOrder(int orderId)
        {
            var order = await _context.Orders.Include(s=>s.OrderItems).ThenInclude(s=>s.Product).FirstOrDefaultAsync(s=>s.Id==orderId);

            if (order.OrderStatus == OrderStatus.Cancelled)
                return "Order already cancelled";

            if (order.OrderStatus == OrderStatus.Delivered)
                return "Delivered orders cannot be cancelled";
            foreach (var item in order.OrderItems) 
            {
                item.Product.StockQuantity += item.Quantity;
            }

            order.OrderStatus = OrderStatus.Cancelled;

            await _context.SaveChangesAsync();
            return "Success";
        }

        public async Task<(Order? order, string? error)> CreateOrder(int userId)
        {
            var cartItems = await _context.CartItems
                .Include(c => c.Product)
                .Where(c => c.UserId == userId)
                .ToListAsync();

            if (!cartItems.Any())
                return (null, "السلة فاضية");

            // تحقق من الكميات الأول
            foreach (var item in cartItems)
            {
                if (item.Quantity > item.Product.StockQuantity)
                    return (null, $"الكمية المطلوبة من {item.Product.Name} غير متاحة، المتاح {item.Product.StockQuantity} فقط");
            }

            double total = 0;
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                OrderStatus = OrderStatus.Pending
            };

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach (var item in cartItems)
            {
                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Product.Price
                };
                await _context.OrderItems.AddAsync(orderItem);
                total += item.Quantity * item.Product.Price;
                item.Product.StockQuantity -= item.Quantity;
            }

            order.TotalPrice = total;
            _context.CartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();

            return (order, null);
        }

        public async Task<List<UserOrdersDto>> GetAllUserOrders(int UserId)
        {
            return await _context.Orders.Where(s=>s.UserId == UserId).Select(s=>new UserOrdersDto {
             Id=s.Id,
             OrderDate=s.OrderDate,
             TotalPrice = s.TotalPrice,
             OrderStatus = s.OrderStatus
            }).ToListAsync();
        }

        public async Task<Order?> GetOrderById(int OrderId)
        {
            return await _context.Orders.SingleOrDefaultAsync(s => s.Id == OrderId);
        }

        public async Task<OrderDetailsDto?> GetOrderDetails(int id)
        {
           return await _context.Orders.Include(s => s.OrderItems).ThenInclude(s => s.Product).Where(s => s.Id == id)
                .Select(s => new OrderDetailsDto
                {
                    OrderId = s.Id,
                    TotalPrice = s.TotalPrice,
                    OrderStatus = s.OrderStatus,

                    Items = s.OrderItems
                    .Select (i=> new OrderItemDto
                    {
                        ProductName = i.Product.Name,
                        Price = i.Price,
                        Quantity = i.Quantity,
                        ItemTotal = i.Quantity * i.Price
                    })
                    .ToList()
                }).FirstOrDefaultAsync();
        }

        public async Task<bool> HasOrders(int UserId)
        {
            return await _context.Orders.AnyAsync(o => o.UserId == UserId);
        }

        public async Task UpdateStatus()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _context.Orders.ToListAsync();
        }
    }
}
