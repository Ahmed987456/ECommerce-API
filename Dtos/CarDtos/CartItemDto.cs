namespace E_Commerce_API.Dtos.CarDtos
{
    public class CartItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double ItemTotal { get; set; }
    }
}
