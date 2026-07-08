namespace E_Commerce_API.Dtos.CategoryDtos
{
    public class CreateCategoryDto
    {
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}