using E_Commerce_API.Dtos.OrdersDto;
using E_Commerce_API.Enums;

public class AdminOrderDto
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public double TotalPrice { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public List<OrderItemDto> Items { get; set; }
}