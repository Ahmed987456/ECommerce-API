using E_Commerce_API.Dtos.CarDtos;

namespace E_Commerce_API.Services.CarServices
{
    public class CarService : ICarService
    {
        private readonly AppDbContext _context;

        public CarService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateCarItem(CreateCartItemDto dto, int userId)
        {
            var CarItem = await _context.CartItems.FirstOrDefaultAsync(
                s => s.UserId == userId && s.ProductId == dto.ProductId
            );
            if (CarItem != null)
            {
                CarItem.Quantity += dto.Quantity;
            }
            else
            {
                var NewItem = new CartItem
                {
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity,
                    UserId = userId,
                };
                await _context.CartItems.AddAsync(NewItem);
            }
            await _context.SaveChangesAsync();
        }

        public async Task  DeleteCarItem(CartItem cartItem)
        {
            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task<CartDto> GetUserCart(int userId)
        {
            var cartItems = await _context.CartItems
                .Include(c => c.Product)
                .Where(c => c.UserId == userId)
                .ToListAsync();

            if (!cartItems.Any())
                return new CartDto
                {
                    Items = new List<CartItemDto>(),
                    TotalPrice = 0
                };

            var items = cartItems.Select(c => new CartItemDto
            {
                ProductName = c.Product.Name,
                Price = c.Product.Price,
                Quantity = c.Quantity,
                ItemTotal = c.Quantity * c.Product.Price
            }).ToList();

            var totalPrice = items.Sum(x => x.ItemTotal);

            return new CartDto
            {
                Items = items,
                TotalPrice = totalPrice
            };
        }

        public async Task<CartItem?> GetCartItem(int userId, int productId)
        {
            return await _context.CartItems
                .FirstOrDefaultAsync(c =>
                    c.UserId == userId &&
                    c.ProductId == productId);
        }

        public async Task UpdateCarItemQuantity(CartItem cartItem)
        {
            _context.CartItems.Update(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserCartItems(int userId)
        {
            var items = await _context.CartItems
            .Where(c => c.UserId == userId)
            .ToListAsync();

            _context.CartItems.RemoveRange(items);

            await _context.SaveChangesAsync();
        }
    }
}
