using Services;

using Microsoft.AspNetCore.Mvc;
using Persistence;
using Shared.ErrorModels;
using Domain.Contracts;
using Store.Fares.Api.MiddleWares;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Persistence.Identity;
using Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Store.Fares.Api.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection RegisterAllServices(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
        {
            services.AddBuiltInServices();

            services.AddSwaggerServices();
    

            services.AddInfrastructureServices(configuration, environment);

            services.AddIdentityServices();

            services.AddApplicationServices();


            services.ConfigureServices();

            services.ConfigureJwtServices(configuration);

            services.AddCors(config =>
            {
                config.AddPolicy("CorsPolicy", options =>
                {
                    options.AllowAnyHeader();
                    options.AllowAnyMethod();
                           // .AllowAnyOrigin()
                   options.WithOrigins("http://localhost:4200");
                });
            });
                
                

            return services;
        }


        private static IServiceCollection AddBuiltInServices(this IServiceCollection services)
        {
            services.AddControllers();


            return services;
        }
        private static IServiceCollection ConfigureJwtServices(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection("jwtOptions").Get<JwtOptions>();

            if (jwtOptions == null)
            {
                throw new InvalidOperationException("JWT Options configuration section is missing from appsettings.json");
            }

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;


            }
            ).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,

                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))

                };
            });


            return services;
        }




        private static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>()
                 .AddEntityFrameworkStores<StoreIdentityDbContext>();


            return services;
        }


        private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();


            return services;
        }

        private static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(config =>
            {
                config.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(m => m.Value?.Errors.Any() == true)
                                              .Select(m => new ValidationError()
                                              {
                                                  Field = m.Key,
                                                  Errors = m.Value!.Errors.Select(errors => errors.ErrorMessage)
                                              }
                                              );

                    var response = new ValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(response);
                };
            }
             );


            return services;
        }


        public static async Task<WebApplication> ConfigureMiddlewares(this WebApplication app)
        {
          await  app.InitializeDatabaseAsync();


            app.UseGlobalErrorHandling();




            // Configure Swagger in all environments to make API docs accessible after deployment.
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseStaticFiles();

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();


            app.UseAuthentication();
            app.UseAuthorization();

            app.MapGet("/", () => Results.Ok(new
            {
                status = "ok",
                message = "Store.Fares API is running",
                docs = "/swagger"
            }));

            app.MapGet("/health", () => Results.Ok(new
            {
                status = "healthy",
                timestamp = DateTime.UtcNow
            }));


            app.MapControllers();

            return app;
        }


        private static async Task<WebApplication> InitializeDatabaseAsync(this WebApplication app)
        {
            try
            {
                using var scope = app.Services.CreateScope();

                var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>(); // ASK CLR Create Object From IDbInitializer
                await dbInitializer.InitializeAsync();
            }
            catch (Exception ex)
            {
                // Log the error but continue with app startup
                Console.WriteLine($"Database initialization failed: {ex.Message}");
            }

            return app;
        }

        private static  WebApplication UseGlobalErrorHandling(this WebApplication app)
        {


            app.UseMiddleware<GlobalErrorHandlingMiddleware>();



            return app;
        }

    }
}
