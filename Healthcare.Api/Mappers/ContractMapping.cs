﻿using AutoMapper;
using Healthcare.Api.Contracts;
using Healthcare.Api.Core.Entities;

namespace Helthcare.Api.Mappers
{
    public class ContractMapping : Profile
    {
        public ContractMapping()
        {
            #region Responses
            //CreateMap<Brand, BrandResponse>().ReverseMap();
            //CreateMap<Category, CategoryResponse>().ReverseMap();
            //CreateMap<Product, ProductResponse>().ReverseMap();
            //CreateMap<SubCategory, SubCategoryResponse>().ReverseMap();
            //CreateMap<Family, FamilyResponse>().ReverseMap();
            #endregion

            #region Requests
            CreateMap<User, UserRequest>().ReverseMap();
            //CreateMap<Category, CategoryRequest>().ReverseMap();
            //CreateMap<Product, ProductRequest>().ReverseMap();
            //CreateMap<SubCategory, SubCategoryRequest>().ReverseMap();
            //CreateMap<Family, FamilyRequest>().ReverseMap();
            #endregion
        }
    }
}
