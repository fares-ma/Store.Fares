using AutoMapper;
using Domain.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductResultDto>()
                .ForMember(d => d.BrandName,o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.TypeName,o => o.MapFrom(s => s.ProductType.Name))
                ;
                
            CreateMap<ProductBrand, BrandResuItDto>();
            CreateMap<ProductType, TypeResuItDto>();
        }

    }
}
