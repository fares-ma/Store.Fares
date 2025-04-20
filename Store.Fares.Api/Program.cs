
using Domain.Contracts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Services;
using Services.Abstractions;
using Shared.ErrorsModels;
using Store.Fares.Api.Extensions;
using Store.Fares.Api.MiddleWares;

namespace Store.Fares.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            builder.Services.RegisterAllServices(builder.Configuration);


            var app = builder.Build();


           await app.ConfigureMiddlewares();

            app.Run();
        }
    }
}
