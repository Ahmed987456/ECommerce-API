using E_Commerce_API.Dtos.CarDtos;
using E_Commerce_API.Dtos.CategoryDtos;
using E_Commerce_API.Dtos.ProductDtos;
using E_Commerce_API.Dtos.UserDtos;
using System.Numerics;

namespace E_Commerce_API.Helpers
 
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<CreateProductDto, Product>();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();
            CreateMap<UpdateCartDto, CartItem>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.ProductId, opt => opt.Ignore());
            CreateMap<CreateUserDto, User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());
            CreateMap<UpdateUserDto, User>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<User, UserDto>();
            CreateMap<Category, CategoryWithCountDto>()
            .ForMember(dest => dest.ProductsCount, opt => opt.MapFrom(src => src.Products.Count()))
            .ForMember(dest => dest.SubCategories, opt => opt.MapFrom(src => src.SubCategories));
        }
    }
}
