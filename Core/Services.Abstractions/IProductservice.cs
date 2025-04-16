using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IProductservice
    {

        // Get All Product
        //Task<IEnumerable<ProductResultDto>> GetAllProductsAsync(int? brandId,int? typeId, string? sort, int pageIndex = 1, int pageSize = 5);

        Task<PaginationResponse<ProductResultDto>> GetAllProductsAsync(ProductSpecificationParamters specParams);

        // Get Product By Id

        Task<ProductResultDto?> GetProductByIdAsync(int id);


        // Get All Types

        Task<IEnumerable<TypeResuItDto>> GetAllTypesAsync();


        // Get All Brands

        Task<IEnumerable<BrandResuItDto>> GetAllBrandsAsync();
    }
}
