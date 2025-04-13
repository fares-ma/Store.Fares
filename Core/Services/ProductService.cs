﻿using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService(IUnitOfWork unitOfWork,IMapper mapper) : IProductservice
    {

        public async Task<IEnumerable<ProductResultDto>> GetAllProductsAsync()
        {
          var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync();
          var result = mapper.Map<IEnumerable<ProductResultDto>>(products);
            return result;
        }
         
        public async Task<ProductResultDto?> GetProductByIdAsync(int id)
        {
            var product = await unitOfWork.GetRepository<Product, int>().GetAsync(id);
            if (product is null)return null;
            var result = mapper.Map<ProductResultDto>(product);
            return result;

        }

        public async Task<IEnumerable<BrandResuItDto>> GetAllbrandsAsync()
        {

            var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            var result = mapper.Map<IEnumerable<BrandResuItDto>>(brands);
            return result;

        }

        public async Task<IEnumerable<TypeResuItDto>> GetAllTypesAsync()
        {
            var types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var result = mapper.Map<IEnumerable<TypeResuItDto>>(types);
            return result;
        }

    }
}
