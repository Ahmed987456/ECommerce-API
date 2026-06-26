using E_Commerce_API.Data;
using E_Commerce_API.Enums;
using E_Commerce_API.Models;
using Microsoft.EntityFrameworkCore;
namespace E_Commerce_API
{
    public static class SeedData
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            Console.WriteLine("=== Seeding Started ===");
            // ===== Categories =====
            var electronics = new Category { Name = "Electronics" };
            var fashion = new Category { Name = "Fashion" };
            var beauty = new Category { Name = "Beauty & Personal Care" };
            var sports = new Category { Name = "Sports & Fitness" };
            var books = new Category { Name = "Books" };

            await context.Categories.AddRangeAsync(electronics, fashion, beauty, sports, books);
            await context.SaveChangesAsync();

            // ===== SubCategories =====
            var mobiles = new Category { Name = "Mobile Phones", ParentCategoryId = electronics.Id };
            var laptops = new Category { Name = "Laptops", ParentCategoryId = electronics.Id };
            var menClothing = new Category { Name = "Men's Clothing", ParentCategoryId = fashion.Id };
            var womenClothing = new Category { Name = "Women's Clothing", ParentCategoryId = fashion.Id };
            var skinCare = new Category { Name = "Skin Care", ParentCategoryId = beauty.Id };
            var hairCare = new Category { Name = "Hair Care", ParentCategoryId = beauty.Id };
            var sportswear = new Category { Name = "Sportswear", ParentCategoryId = sports.Id };
            var fitnessEquipment = new Category { Name = "Fitness Equipment", ParentCategoryId = sports.Id };
            var educationalBooks = new Category { Name = "Educational Books", ParentCategoryId = books.Id };

            await context.Categories.AddRangeAsync(
                mobiles, laptops, menClothing, womenClothing,
                skinCare, hairCare, sportswear, fitnessEquipment, educationalBooks
            );
            await context.SaveChangesAsync();

            // ===== Products =====
            var products = new List<Product>
            {
                new Product
                {
                    Name = "Apple iPhone 16 Pro",
                    Description = "Latest Apple flagship with A18 Pro chip, 48MP camera system, and titanium design.",
                    Price = 58000,
                    StockQuantity = 15,
                    ImageUrl = "https://images.unsplash.com/photo-1591337676887-a217a6970a8a?w=800",
                    CategoryId = mobiles.Id
                },
                new Product
                {
                    Name = "Samsung Galaxy S25",
                    Description = "Samsung's latest flagship with Snapdragon 8 Elite, 200MP camera, and AI features.",
                    Price = 45000,
                    StockQuantity = 20,
                    ImageUrl = "https://images.unsplash.com/photo-1610945415295-d9bbf067e59c?w=800",
                    CategoryId = mobiles.Id
                },
                new Product
                {
                    Name = "MacBook Pro M4",
                    Description = "Powerful laptop with Apple M4 chip, 16GB RAM, and 512GB SSD.",
                    Price = 95000,
                    StockQuantity = 8,
                    ImageUrl = "https://images.unsplash.com/photo-1517336714731-489689fd1ca8?w=800",
                    CategoryId = laptops.Id
                },
                new Product
                {
                    Name = "Dell XPS 15",
                    Description = "Premium Windows laptop with Intel Core i9, OLED display, and RTX 4070.",
                    Price = 75000,
                    StockQuantity = 10,
                    ImageUrl = "https://images.unsplash.com/photo-1593642632559-0c6d3fc62b89?w=800",
                    CategoryId = laptops.Id
                },
                new Product
                {
                    Name = "Nike Air Force 1",
                    Description = "Classic Nike sneakers, comfortable and stylish for everyday wear.",
                    Price = 6500,
                    StockQuantity = 30,
                    ImageUrl = "https://images.unsplash.com/photo-1600185365483-26d7a4cc7519?w=800",
                    CategoryId = menClothing.Id
                },
                new Product
                {
                    Name = "Levi's 501 Jeans",
                    Description = "Original straight fit jeans, timeless American style.",
                    Price = 4500,
                    StockQuantity = 25,
                    ImageUrl = "https://images.unsplash.com/photo-1542272604-787c3835535d?w=800",
                    CategoryId = menClothing.Id
                },
                new Product
                {
                    Name = "Zara Floral Dress",
                    Description = "Elegant floral summer dress, perfect for any occasion.",
                    Price = 3200,
                    StockQuantity = 18,
                    ImageUrl = "https://images.unsplash.com/photo-1595777457583-95e059d581b8?w=800",
                    CategoryId = womenClothing.Id
                },
                new Product
                {
                    Name = "The Ordinary Niacinamide Serum",
                    Description = "10% Niacinamide + 1% Zinc serum for blemish and congestion control.",
                    Price = 850,
                    StockQuantity = 50,
                    ImageUrl = "https://images.unsplash.com/photo-1556228578-8c89e6adf883?w=800",
                    CategoryId = skinCare.Id
                },
                new Product
                {
                    Name = "Cetaphil Moisturizing Cream",
                    Description = "Gentle moisturizing cream suitable for sensitive skin.",
                    Price = 1200,
                    StockQuantity = 40,
                    ImageUrl = "https://images.unsplash.com/photo-1570194065650-d99fb4b8ccb0?w=800",
                    CategoryId = skinCare.Id
                },
                new Product
                {
                    Name = "Nike Dri-FIT T-Shirt",
                    Description = "Lightweight moisture-wicking sportswear for training and running.",
                    Price = 2800,
                    StockQuantity = 35,
                    ImageUrl = "https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?w=800",
                    CategoryId = sportswear.Id
                },
                new Product
                {
                    Name = "Adidas Ultraboost Running Shoes",
                    Description = "High-performance running shoes with Boost cushioning technology.",
                    Price = 9500,
                    StockQuantity = 12,
                    ImageUrl = "https://images.unsplash.com/photo-1542291026-7eec264c27ff?w=800",
                    CategoryId = sportswear.Id
                },
                new Product
                {
                    Name = "Adjustable Dumbbell Set",
                    Description = "Space-saving adjustable dumbbells from 5kg to 30kg per dumbbell.",
                    Price = 8500,
                    StockQuantity = 7,
                    ImageUrl = "https://images.unsplash.com/photo-1534438327276-14e5300c3a48?w=800",
                    CategoryId = fitnessEquipment.Id
                },
                new Product
                {
                    Name = "Clean Code — Robert C. Martin",
                    Description = "A handbook of agile software craftsmanship for developers.",
                    Price = 650,
                    StockQuantity = 45,
                    ImageUrl = "https://images.unsplash.com/photo-1532012197267-da84d127e765?w=800",
                    CategoryId = educationalBooks.Id
                },
                new Product
                {
                    Name = "The Pragmatic Programmer",
                    Description = "Your journey to mastery — essential reading for every developer.",
                    Price = 580,
                    StockQuantity = 38,
                    ImageUrl = "https://images.unsplash.com/photo-1543002588-bfa74002ed7e?w=800",
                    CategoryId = educationalBooks.Id
                },
            };

            await context.Products.AddRangeAsync(products);

            await context.SaveChangesAsync();
            // ===== Admin User =====
            if (!context.Users.Any(u => u.Role == UserRole.Admin))
            {
                var admin = new User
                {
                    Name = "Ahmed Oraby",
                    Email = "ahmedoraby57000@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("123456789"),
                    Role = UserRole.Admin
                };
                await context.Users.AddAsync(admin);
                await context.SaveChangesAsync();
            }
        }
    }
}