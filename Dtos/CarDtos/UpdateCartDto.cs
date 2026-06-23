using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API.Dtos.CarDtos
{
    public class UpdateCartDto
    {

        public int ProductId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero")]
        public int Quantity { get; set; }
    }
}
