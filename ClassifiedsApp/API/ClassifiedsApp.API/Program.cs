using ClassifiedsApp.API.Config;
using ClassifiedsApp.API.Middlewares;
using ClassifiedsApp.Application;
using ClassifiedsApp.Core.Entities;
using ClassifiedsApp.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Host.UseSerilog((context, config) =>
{
	config.ReadFrom.Configuration(context.Configuration);
});

builder.Services.AuthenticationAndAuthorization(builder.Configuration);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddSwagger();

var client = builder.Configuration["ClientUrl"];

builder.Services.AddCors(options => options.AddPolicy("CORSPolicy", builder =>
{
	builder.WithOrigins(client!)
		   .AllowAnyMethod()
		   .AllowAnyHeader()
		   .AllowCredentials();
}));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseCors("CORSPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Seed roles and admin user on application startup
using (var scope = app.Services.CreateScope())
{
	var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
	var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

	await SeedData.SeedRolesAndUsersAsync(roleManager, userManager, builder.Configuration);
}

app.Run();



//-----------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------

/*

public class NotFoundUserException : Exception
	{
		public NotFoundUserException() : base("Kullanıcı adı veya şifre hatalı.")
		{
		}

		public NotFoundUserException(string? message) : base(message)
		{
		}

		public NotFoundUserException(string? message, Exception? innerException) : base(message, innerException)
		{
		}
	}

*/

/*


// Infrastructure/Cache/Models/CacheConfiguration.cs
namespace YourProject.Infrastructure.Cache.Models
{
	public class CacheConfiguration
	{
		public string ConnectionString { get; set; } = string.Empty;
		public string InstanceName { get; set; } = string.Empty;
		public int AbsoluteExpirationInHours { get; set; }
		public int SlidingExpirationInMinutes { get; set; }
	}
}

// Infrastructure/Cache/Interfaces/ICacheService.cs
namespace YourProject.Infrastructure.Cache.Interfaces
{
	public interface ICacheService
	{
		Task<T?> GetAsync<T>(string key);
		Task SetAsync<T>(string key, T value);
		Task SetAsync<T>(string key, T value, TimeSpan expiration);
		Task RemoveAsync(string key);
		Task<bool> ExistsAsync(string key);
		Task RemoveByPrefixAsync(string prefix);
	}
}

// Infrastructure/Cache/Services/RedisCacheService.cs
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourProject.Infrastructure.Cache.Interfaces;
using YourProject.Infrastructure.Cache.Models;

namespace YourProject.Infrastructure.Cache.Services
{
	public class RedisCacheService : ICacheService
	{
		private readonly IConnectionMultiplexer _redisConnection;
		private readonly IDatabase _database;
		private readonly ILogger<RedisCacheService> _logger;
		private readonly CacheConfiguration _cacheConfig;
		private readonly string _instanceName;

		public RedisCacheService(
			IConnectionMultiplexer redisConnection,
			IOptions<CacheConfiguration> cacheConfig,
			ILogger<RedisCacheService> logger)
		{
			_redisConnection = redisConnection;
			_database = redisConnection.GetDatabase();
			_logger = logger;
			_cacheConfig = cacheConfig.Value;
			_instanceName = _cacheConfig.InstanceName;
		}

		private string GetKey(string key) => $"{_instanceName}:{key}";

		public async Task<T?> GetAsync<T>(string key)
		{
			string fullKey = GetKey(key);

			try
			{
				RedisValue value = await _database.StringGetAsync(fullKey);

				if (value.IsNullOrEmpty)
				{
					_logger.LogInformation("Cache miss for key: {Key}", key);
					return default;
				}

				_logger.LogInformation("Cache hit for key: {Key}", key);
				return JsonConvert.DeserializeObject<T>(value.ToString());
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error retrieving cache value for key: {Key}", key);
				return default;
			}
		}

		public async Task SetAsync<T>(string key, T value)
		{
			TimeSpan expiration = TimeSpan.FromHours(_cacheConfig.AbsoluteExpirationInHours);
			await SetAsync(key, value, expiration);
		}

		public async Task SetAsync<T>(string key, T value, TimeSpan expiration)
		{
			string fullKey = GetKey(key);

			try
			{
				string serializedValue = JsonConvert.SerializeObject(value);
				await _database.StringSetAsync(fullKey, serializedValue, expiration);
				_logger.LogInformation("Cache set for key: {Key} with expiration: {Expiration}", key, expiration);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error setting cache value for key: {Key}", key);
			}
		}

		public async Task RemoveAsync(string key)
		{
			string fullKey = GetKey(key);

			try
			{
				await _database.KeyDeleteAsync(fullKey);
				_logger.LogInformation("Cache removed for key: {Key}", key);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error removing cache value for key: {Key}", key);
			}
		}

		public async Task<bool> ExistsAsync(string key)
		{
			string fullKey = GetKey(key);

			try
			{
				return await _database.KeyExistsAsync(fullKey);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error checking cache existence for key: {Key}", key);
				return false;
			}
		}

		public async Task RemoveByPrefixAsync(string prefix)
		{
			string pattern = $"{_instanceName}:{prefix}*";

			try
			{
				var endpoints = _redisConnection.GetEndPoints();
				var server = _redisConnection.GetServer(endpoints.First());
				var keys = server.Keys(pattern: pattern);

				foreach (var key in keys)
				{
					await _database.KeyDeleteAsync(key);
				}

				_logger.LogInformation("Cache removed for pattern: {Pattern}", pattern);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error removing cache by prefix: {Prefix}", prefix);
			}
		}
	}
}

// Infrastructure/Cache/Extensions/CacheServiceExtensions.cs
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using YourProject.Infrastructure.Cache.Interfaces;
using YourProject.Infrastructure.Cache.Models;
using YourProject.Infrastructure.Cache.Services;

namespace YourProject.Infrastructure.Cache.Extensions
{
	public static class CacheServiceExtensions
	{
		public static IServiceCollection AddRedisCacheServices(this IServiceCollection services, IConfiguration configuration)
		{
			var cacheConfig = new CacheConfiguration();
			configuration.GetSection("RedisCache").Bind(cacheConfig);
			services.Configure<CacheConfiguration>(configuration.GetSection("RedisCache"));

			services.AddSingleton<IConnectionMultiplexer>(sp =>
				ConnectionMultiplexer.Connect(cacheConfig.ConnectionString));

			services.AddSingleton<ICacheService, RedisCacheService>();

			return services;
		}
	}
}

// Application/Common/Behaviors/CachingBehavior.cs
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using YourProject.Application.Common.Attributes;
using YourProject.Application.Common.Interfaces;
using YourProject.Infrastructure.Cache.Interfaces;

namespace YourProject.Application.Common.Behaviors
{
	public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
	{
		private readonly ICacheService _cacheService;
		private readonly ILogger<CachingBehavior<TRequest, TResponse>> _logger;

		public CachingBehavior(ICacheService cacheService, ILogger<CachingBehavior<TRequest, TResponse>> logger)
		{
			_cacheService = cacheService;
			_logger = logger;
		}

		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			// Check if request has cache attribute
			var cacheAttribute = (CacheAttribute?)Attribute.GetCustomAttribute(
				request.GetType(), typeof(CacheAttribute));

			if (cacheAttribute == null)
			{
				// No cache attribute, proceed with normal execution
				return await next();
			}

			// Generate a cache key based on the request type and values
			string cacheKey = GenerateCacheKey(request, cacheAttribute.CacheKeyPrefix);

			// Try to get cached response
			var cachedResponse = await _cacheService.GetAsync<TResponse>(cacheKey);
			if (cachedResponse != null)
			{
				_logger.LogInformation("Returning cached response for {RequestType}", typeof(TRequest).Name);
				return cachedResponse;
			}

			// If not cached, execute the request handler
			var response = await next();

			// Cache the response
			TimeSpan expiration = TimeSpan.FromMinutes(cacheAttribute.ExpirationInMinutes);
			await _cacheService.SetAsync(cacheKey, response, expiration);

			return response;
		}

		private string GenerateCacheKey(TRequest request, string keyPrefix)
		{
			// Create a cache key based on the request type and property values
			// You might want to customize this based on your needs
			string requestTypeName = request.GetType().Name;
			string requestHash = JsonSerializer.Serialize(request).GetHashCode().ToString();
			return $"{keyPrefix}:{requestTypeName}:{requestHash}";
		}
	}
}

// Application/Common/Attributes/CacheAttribute.cs
using System;

namespace YourProject.Application.Common.Attributes
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class CacheAttribute : Attribute
	{
		public string CacheKeyPrefix { get; }
		public int ExpirationInMinutes { get; }

		public CacheAttribute(string cacheKeyPrefix, int expirationInMinutes = 30)
		{
			CacheKeyPrefix = cacheKeyPrefix;
			ExpirationInMinutes = expirationInMinutes;
		}
	}
}

// Application/Common/Attributes/InvalidateCacheAttribute.cs
using System;

namespace YourProject.Application.Common.Attributes
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class InvalidateCacheAttribute : Attribute
	{
		public string CacheKeyPrefix { get; }

		public InvalidateCacheAttribute(string cacheKeyPrefix)
		{
			CacheKeyPrefix = cacheKeyPrefix;
		}
	}
}

// Application/Common/Behaviors/CacheInvalidationBehavior.cs
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using YourProject.Application.Common.Attributes;
using YourProject.Infrastructure.Cache.Interfaces;

namespace YourProject.Application.Common.Behaviors
{
	public class CacheInvalidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
	{
		private readonly ICacheService _cacheService;
		private readonly ILogger<CacheInvalidationBehavior<TRequest, TResponse>> _logger;

		public CacheInvalidationBehavior(ICacheService cacheService, ILogger<CacheInvalidationBehavior<TRequest, TResponse>> logger)
		{
			_cacheService = cacheService;
			_logger = logger;
		}

		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			// Execute the handler
			TResponse response = await next();

			// Check if request has cache invalidation attributes
			var invalidationAttributes = (InvalidateCacheAttribute[])Attribute.GetCustomAttributes(
				request.GetType(), typeof(InvalidateCacheAttribute));

			if (invalidationAttributes.Length > 0)
			{
				foreach (var attribute in invalidationAttributes)
				{
					await _cacheService.RemoveByPrefixAsync(attribute.CacheKeyPrefix);
					_logger.LogInformation("Cache invalidated for prefix: {Prefix} by {RequestType}",
						attribute.CacheKeyPrefix, typeof(TRequest).Name);
				}
			}

			return response;
		}
	}
}

// Application/Example/Queries/GetProductListQuery.cs
using MediatR;
using YourProject.Application.Common.Attributes;
using YourProject.Domain.Entities;

namespace YourProject.Application.Example.Queries
{
	[Cache("products", 60)] // Cache for 60 minutes
	public class GetProductListQuery : IRequest<List<Product>>
	{
		public string? CategoryFilter { get; set; }
	}
}

// Application/Example/Queries/GetProductListQueryHandler.cs
using MediatR;
using YourProject.Application.Common.Interfaces;
using YourProject.Domain.Entities;

namespace YourProject.Application.Example.Queries
{
	public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, List<Product>>
	{
		private readonly IProductRepository _productRepository;

		public GetProductListQueryHandler(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		public async Task<List<Product>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
		{
			return await _productRepository.GetProductsAsync(request.CategoryFilter, cancellationToken);
		}
	}
}

// Application/Example/Commands/CreateProductCommand.cs
using MediatR;
using YourProject.Application.Common.Attributes;
using YourProject.Domain.Entities;

namespace YourProject.Application.Example.Commands
{
	[InvalidateCache("products")] // This will invalidate the product list cache
	public class CreateProductCommand : IRequest<Product>
	{
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public decimal Price { get; set; }
		public string Category { get; set; } = string.Empty;
	}
}

// Application/Example/Commands/CreateProductCommandHandler.cs
using MediatR;
using YourProject.Application.Common.Interfaces;
using YourProject.Domain.Entities;

namespace YourProject.Application.Example.Commands
{
	public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
	{
		private readonly IProductRepository _productRepository;

		public CreateProductCommandHandler(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
		{
			var product = new Product
			{
				Name = request.Name,
				Description = request.Description,
				Price = request.Price,
				Category = request.Category
			};

			return await _productRepository.AddProductAsync(product, cancellationToken);
		}
	}
}

// Program.cs (or Startup.cs) - Registration of services
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using YourProject.Application.Common.Behaviors;
using YourProject.Infrastructure.Cache.Extensions;
using MediatR;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container
		builder.Services.AddControllers();

		// Register Redis Cache
		builder.Services.AddRedisCacheServices(builder.Configuration);

		// Register MediatR
		builder.Services.AddMediatR(cfg => {
		cfg.RegisterServicesFromAssembly(typeof(YourProject.Application.DependencyInjection).Assembly);

		// Add pipeline behaviors for caching
			cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
			cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(CacheInvalidationBehavior<,>));

			// Register other behaviors (ValidationBehavior, LoggingBehavior, etc.)
		});

		// Add other services...

		var app = builder.Build();

		// Configure the HTTP request pipeline
		if (app.Environment.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
		}

		app.UseHttpsRedirection();
		app.UseRouting();
		app.UseAuthorization();
		app.MapControllers();

		app.Run();
	}
}

// appsettings.json - Redis configuration
// Add this to your appsettings.json

{
  "RedisCache": {
	"ConnectionString": "localhost:6379",
	"InstanceName": "YourAppName",
	"AbsoluteExpirationInHours": 1,
	"SlidingExpirationInMinutes": 30
  },
  "Logging": {
	"LogLevel": {
	  "Default": "Information",
	  "Microsoft": "Warning",
	  "Microsoft.Hosting.Lifetime": "Information"
	}
  },
  "AllowedHosts": "*"
}


// Controllers/ProductsController.cs - Example of controller using CQRS with Redis cache
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YourProject.Application.Example.Commands;
using YourProject.Application.Example.Queries;
using YourProject.Domain.Entities;

namespace YourProject.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductsController : ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly ILogger<ProductsController> _logger;

		public ProductsController(IMediator mediator, ILogger<ProductsController> logger)
		{
			_mediator = mediator;
			_logger = logger;
		}

		[HttpGet]
		public async Task<ActionResult<List<Product>>> GetProducts([FromQuery] string? category = null)
		{
			_logger.LogInformation("Getting products with category filter: {Category}", category ?? "All");

			var query = new GetProductListQuery { CategoryFilter = category };
			var result = await _mediator.Send(query);

			return Ok(result);
		}

		[HttpPost]
		public async Task<ActionResult<Product>> CreateProduct([FromBody] CreateProductCommand command)
		{
			_logger.LogInformation("Creating new product: {ProductName}", command.Name);

			var result = await _mediator.Send(command);

			return CreatedAtAction(nameof(GetProduct), new { id = result.Id }, result);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProduct(int id)
		{
			_logger.LogInformation("Getting product with id: {ProductId}", id);

			var query = new GetProductByIdQuery { Id = id };
			var result = await _mediator.Send(query);

			if (result == null)
			{
				return NotFound();
			}

			return Ok(result);
		}
	}
}

// Application/Example/Queries/GetProductByIdQuery.cs
using MediatR;
using YourProject.Application.Common.Attributes;
using YourProject.Domain.Entities;

namespace YourProject.Application.Example.Queries
{
	[Cache("product", 60)] // Cache for 60 minutes
	public class GetProductByIdQuery : IRequest<Product?>
	{
		public int Id { get; set; }
	}
}

// Application/Example/Queries/GetProductByIdQueryHandler.cs
using MediatR;
using YourProject.Application.Common.Interfaces;
using YourProject.Domain.Entities;

namespace YourProject.Application.Example.Queries
{
	public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product?>
	{
		private readonly IProductRepository _productRepository;

		public GetProductByIdQueryHandler(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		public async Task<Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
		{
			return await _productRepository.GetProductByIdAsync(request.Id, cancellationToken);
		}
	}
}

// Application/Common/Interfaces/IProductRepository.cs
using YourProject.Domain.Entities;

namespace YourProject.Application.Common.Interfaces
{
	public interface IProductRepository
	{
		Task<List<Product>> GetProductsAsync(string? category, CancellationToken cancellationToken);
		Task<Product?> GetProductByIdAsync(int id, CancellationToken cancellationToken);
		Task<Product> AddProductAsync(Product product, CancellationToken cancellationToken);
	}
}

// Infrastructure/Persistence/Repositories/ProductRepository.cs
using Microsoft.EntityFrameworkCore;
using YourProject.Application.Common.Interfaces;
using YourProject.Domain.Entities;
using YourProject.Infrastructure.Persistence.Context;

namespace YourProject.Infrastructure.Persistence.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly ApplicationDbContext _dbContext;

		public ProductRepository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<List<Product>> GetProductsAsync(string? category, CancellationToken cancellationToken)
		{
			var query = _dbContext.Products.AsQueryable();

			if (!string.IsNullOrEmpty(category))
			{
				query = query.Where(p => p.Category == category);
			}

			return await query.ToListAsync(cancellationToken);
		}

		public async Task<Product?> GetProductByIdAsync(int id, CancellationToken cancellationToken)
		{
			return await _dbContext.Products.FindAsync(new object[] { id }, cancellationToken);
		}

		public async Task<Product> AddProductAsync(Product product, CancellationToken cancellationToken)
		{
			_dbContext.Products.Add(product);
			await _dbContext.SaveChangesAsync(cancellationToken);
			return product;
		}
	}
}

// Domain/Entities/Product.cs
namespace YourProject.Domain.Entities
{
	public class Product
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public decimal Price { get; set; }
		public string Category { get; set; } = string.Empty;
	}
}



*/