namespace E_Commerce_API.Dtos.CarDtos
{
    public class CartDto
    {
        public List<CartItemDto> Items { get; set; }
        public double TotalPrice { get; set; }

        public List<string> Warnings { get; set; } = new List<string>();
    }
}
