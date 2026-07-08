namespace E_Commerce_API.Dtos.CategoryDtos
{
    public class CategoryWithCountDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductsCount { get; set; }
        public List<CategoryWithCountDto> SubCategories { get; set; }
    }
}