﻿using AutoMapper;
using Domain.Contracts;
using Services.Abstractions;
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
        IBasketRepository basketRepository
        ) : IServiceManager
    {
        public IProductservice ProductService { get; } = new ProductService(unitOfWork,mapper);

        public IBasketService BasketService { get; } = new BasketService(basketRepository, mapper);
    }
}
