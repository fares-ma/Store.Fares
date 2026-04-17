using AutoMapper;
using Domain.Contracts;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IBasketRepository basketRepository,
        UserManager<AppUser> userManager,
        IOptions<JwtOptions> options,
        IConfiguration configuration,
        ICacheRepository cacheRepository
        ) : IServiceManager
    {
        public IProductservice ProductService { get; } = new ProductService(unitOfWork,mapper);

        public IBasketService BasketService { get; } = new BasketService(basketRepository, mapper);
        
        public IAuthService AuthService { get; } = new AuthService(userManager, options, mapper);
        
        public IOrderService OrderService { get; } = new OrderService(mapper, basketRepository, unitOfWork);
        
        public IPaymentService PaymentService { get; } = new PaymentService(basketRepository, unitOfWork, mapper, configuration);
        
        public ICacheService CacheService { get; } = new CacheService(cacheRepository);
    }
}
