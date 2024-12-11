using AutoMapper;
using DAL.Models.DTOs;
using DAL.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add your existing mappings for User
            CreateMap<UserRegistrationDTO, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()); // We will handle password hashing separately

            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();  // Add this line to map UserDTO to User

            CreateMap<UserLoginDTO, User>(); // Ensure this is in place for user login

            // Add mappings for Category
            CreateMap<Category, CategoryDTO>(); // Mapping Category to CategoryDTO
            CreateMap<CategoryCreateDTO, Category>(); // Mapping CategoryCreateDTO to Category
            CreateMap<CategoryUpdateDTO, Category>(); // Mapping CategoryUpdateDTO to Category

            // Add Book mappings
            CreateMap<Book, BookDTO>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<BookCreateDTO, Book>();
            CreateMap<BookUpdateDTO, Book>();
        }
    }

}
