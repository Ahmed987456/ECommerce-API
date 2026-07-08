using E_Commerce_API.Dtos.ProductDtos;
namespace E_Commerce_API.Dtos.CategoryDtos
{
    public class CategoryDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductsCount { get; set; }
        public List<ProductDetailsToCategoryById> Products { get; set; }
        public List<CategoryDto> SubCategories { get; set; }
    }
}