

using E_Commerce_API.Dtos.CategoryDtos;
using E_Commerce_API.Dtos.ProductDtos;

namespace E_Commerce_API.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CategoryService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CategoryWithCountDto>> GetAllCategories()
        {
            var categories = await _context.Categories
                .Include(s => s.Products)
                .Include(s => s.SubCategories)
                    .ThenInclude(s => s.Products)
                .Where(c => c.ParentCategoryId == null) // بس الـ Parent Categories
                .ToListAsync();
            return _mapper.Map<List<CategoryWithCountDto>>(categories);
        }

        public async Task<CategoryDetailsDto?> GetCategoryDetails(int id)
        {
            return await _context.Categories
                .Where(c => c.Id == id)
                .Select(c => new CategoryDetailsDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    ProductsCount = c.Products.Count(),
                    Products = c.Products.Select(p => new ProductDetailsToCategoryById
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                        ImageUrl = p.ImageUrl,
                    }).ToList(),
                    SubCategories = c.SubCategories.Select(s => new CategoryDto
                    {
                        Id = s.Id,
                        Name = s.Name
                    }).ToList()
                })
                .SingleOrDefaultAsync();
        }

        public async Task<Category> CreateCategory(Category category)
        { 
           await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> CategoryExists(string Name)
        {
            return await _context.Categories.AnyAsync(c => c.Name.ToLower().Trim() == Name.ToLower().Trim());
        }

        public async Task<bool> CategoryExistsForUpdate(string Name, int id)
        {
            return await _context.Categories.AnyAsync(c => c.Name.ToLower() == Name.ToLower() && c.Id != id);
        }

        public async Task<Category> UpdateCategory(Category category)
        {
             _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task DeleteCategory(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<Category?> GetById(int id)
        {
            return await _context.Categories.Include(c => c.SubCategories).SingleOrDefaultAsync(c => c.Id == id);
        }
    }
}
