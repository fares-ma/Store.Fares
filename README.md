# Store.Fares

Store.Fares is a modular ASP.NET Core 10 e-commerce API built with clean separation between presentation, application, domain, infrastructure, and shared contracts. It exposes product browsing, basket management, authentication, order placement, and payment intent workflows behind a JWT-secured API.

## Live Demo

- Base URL: http://faresstore.runasp.net/
- Swagger UI: http://faresstore.runasp.net/swagger/index.html
- Health Endpoint: http://faresstore.runasp.net/health

## Highlights

- Layered architecture with clear separation of concerns.
- JWT authentication and ASP.NET Core Identity for user accounts.
- Product catalog with filtering, sorting, and pagination.
- Basket, order, and payment flows backed by service abstractions.
- Global error handling and consistent validation responses.
- Swagger/OpenAPI support for interactive API exploration.
- Development-friendly in-memory database fallback when SQL Server is not available locally.

## Tech Stack

- .NET 10 / ASP.NET Core Web API
- Entity Framework Core 10
- SQL Server for production persistence
- In-memory EF Core database for local development
- Redis integration for basket/cache scenarios
- JWT Bearer authentication
- AutoMapper
- Swagger / Swashbuckle

## Solution Structure

- [Store.Fares.Api](Store.Fares.Api) - API host, middleware, and endpoint configuration.
- [Infrastructure/Persistence](Infrastructure/Persistence) - DbContext, repositories, seeding, and infrastructure registration.
- [Core/Services](Core/Services) - application services and business logic.
- [Core/Services.Abstractions](Core/Services.Abstractions) - service interfaces.
- [Core/Domain](Core/Domain) - domain models, contracts, and exceptions.
- [Shared](Shared) - DTOs and API shared contracts.

## Main Features

- Authentication
	- Register, login, current user, and address management.
- Catalog
	- List products with paging and filtering.
	- Get product details by id.
	- Browse brands and types.
- Basket
	- Create, update, and delete baskets.
- Orders
	- Create orders and retrieve a user's order history.
	- Retrieve delivery methods.
- Payments
	- Create or update payment intents for baskets.

## Running Locally

1. Restore and build the solution:

	 ```bash
	 dotnet build
	 ```

2. Run the API project:

	 ```bash
	 dotnet run --project Store.Fares.Api/Store.Fares.Api.csproj
	 ```

3. Open Swagger in the browser:

	 ```text
	 http://localhost:5136/swagger
	 ```

4. Quick smoke tests:

	 ```text
	 GET http://localhost:5136/
	 GET http://localhost:5136/health
	 GET http://localhost:5136/api/products?pageIndex=1&pageSize=3
	 ```

## Local Development Notes

- In Development, the app uses an in-memory database and seeds sample catalog data automatically.
- In Production, configure a real SQL Server connection string in [Store.Fares.Api/appsettings.Production.json](Store.Fares.Api/appsettings.Production.json).
- Configure Redis for production or adjust the basket/cache behavior if Redis is unavailable.

## API Surface

- `POST /api/auth/register`
- `POST /api/auth/login`
- `GET /api/auth`
- `GET /api/products`
- `GET /api/products/{id}`
- `GET /api/products/brands`
- `GET /api/products/types`
- `GET /api/baskets`
- `POST /api/baskets`
- `DELETE /api/baskets`
- `POST /api/orders`
- `GET /api/orders`
- `GET /api/orders/{id}`
- `GET /api/orders/DeliveryMethods`
- `POST /api/payments/{basketId}`

## Deployment

For deployment, set production connection strings and JWT values in [Store.Fares.Api/appsettings.Production.json](Store.Fares.Api/appsettings.Production.json), then publish from Visual Studio or the .NET SDK.

Recommended production workflow:

- Keep credentials out of source control.
- Configure database credentials through host settings or deployment-time transforms.
- Verify post-deploy endpoints:
	- `/`
	- `/health`
	- `/swagger/index.html`