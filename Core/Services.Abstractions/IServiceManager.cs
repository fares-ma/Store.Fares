using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IServiceManager
    {
        IProductservice ProductService { get; }
        //ITypeService TypeService { get; }
        //IBrandService BrandService { get; }
        //Task SaveAsync();
    }
}
