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
        Task<IEnumerable<ProductResultDto>> GetAllProductsAsync();
        Task<ProductResultDto?> GetProductByIdAsync(int id);
     
        Task<IEnumerable<BrandResuItDto>> GetAllbrandsAsync();
        Task<IEnumerable<TypeResuItDto>> GetAllTypesAsync();
    }
}
