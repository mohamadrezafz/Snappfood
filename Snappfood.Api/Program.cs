using Snappfood.Api.Middleware;
using Snappfood.Application.Interfaces.Caching;
using Snappfood.Infrastructure;
using Snappfood.Infrastructure.Persistance;
using Snappfood.Infrastructure.Services.Caching;
using Snappfood.Application;
using Snappfood.Application.Interfaces.Repositories;
using Snappfood.Application.UseCases.Product;
using Snappfood.Infrastructure.Services.Repositories;
using Snappfood.Application.UseCases.Order;
using Snappfood.Application.UseCases.User;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;
builder.Services.AddInfrastructureServices(configuration);
builder.Services.AddApplicationServices();


builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ICacheProvider, MemoryCacheProvider>();
builder.Services.AddScoped<IProductCacheService, ProductCacheService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<AddProductUseCase>();
builder.Services.AddScoped<UpdateProductUseCase>();
builder.Services.AddScoped<GetProductByIdUseCase>();
builder.Services.AddScoped<AddOrderUseCase>();
builder.Services.AddScoped<GetAllUsersUseCase>();

builder.Services.AddMemoryCache();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Snappfood APIs Version 1");
    });

    // Initialize and seed database
    using var scope = app.Services.CreateScope();
    var dbInitialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
    await dbInitialiser.InitialiseAsync();
}
app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();

//app.UseAuthorization();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();

app.Run();
