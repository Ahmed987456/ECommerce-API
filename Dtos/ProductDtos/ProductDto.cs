using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API.Dtos.ProductDtos
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public int StockQuantity { get; set; }

        public string ImageUrl { get; set; }

        public string CategoryName { get; set; }
        public int CategoryId { get; set; }

        public string Description { get; set; }
    }
}
