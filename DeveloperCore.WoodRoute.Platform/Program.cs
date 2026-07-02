using DeveloperCore.WoodRoute.Platform.Engagement.Application.Internal.CommandServices;
using DeveloperCore.WoodRoute.Platform.Engagement.Application.Internal.QueryServices;
using DeveloperCore.WoodRoute.Platform.Engagement.Application.OutboundServices;
using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Repositories;
using DeveloperCore.WoodRoute.Platform.Engagement.Infrastructure.OutboundServices;
using DeveloperCore.WoodRoute.Platform.Engagement.Infrastructure.Persistence.EFC.Repositories;
using DeveloperCore.WoodRoute.Platform.Inventory.Application.CommandServices;
using DeveloperCore.WoodRoute.Platform.Inventory.Application.Internal.CommandServices;
using DeveloperCore.WoodRoute.Platform.Inventory.Application.Internal.QueryServices;
using DeveloperCore.WoodRoute.Platform.Inventory.Application.QueryServices;
using DeveloperCore.WoodRoute.Platform.Inventory.Domain.Repositories;
using DeveloperCore.WoodRoute.Platform.Inventory.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Application.Internal.CommandServices;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Application.Internal.QueryServices;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Repositories;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Infrastructure.Persistence.EFC.Repositories;
using DeveloperCore.WoodRoute.Platform.Profiles.Application.CommandServices;
using DeveloperCore.WoodRoute.Platform.Profiles.Application.Internal.CommandServices;
using DeveloperCore.WoodRoute.Platform.Profiles.Application.Internal.QueryServices;
using DeveloperCore.WoodRoute.Platform.Profiles.Application.QueryServices;
using DeveloperCore.WoodRoute.Platform.Profiles.Domain.Repositories;
using DeveloperCore.WoodRoute.Platform.Profiles.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using DeveloperCore.WoodRoute.Platform.Sales.Application.CommandServices;
using DeveloperCore.WoodRoute.Platform.Sales.Application.Internal.CommandServices;
using DeveloperCore.WoodRoute.Platform.Sales.Application.Internal.QueryServices;
using DeveloperCore.WoodRoute.Platform.Sales.Application.QueryServices;
using DeveloperCore.WoodRoute.Platform.Sales.Domain.Repositories;
using DeveloperCore.WoodRoute.Platform.Sales.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Repositories;
using DeveloperCore.WoodRoute.Platform.Shared.Infrastructure.Interfaces.AspNetCore.Configuration;
using DeveloperCore.WoodRoute.Platform.Shared.Infrastructure.Interfaces.AspNetCore.Configuration.Extensions;
using DeveloperCore.WoodRoute.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using DeveloperCore.WoodRoute.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

// Add Database Connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (connectionString == null)
{
    throw new InvalidOperationException("Connection string not found.");
}

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        options.UseNpgsql(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    }
    else if (builder.Environment.IsProduction())
    {
        options.UseNpgsql(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Error);
    }
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.EnableAnnotations());

// Internationalisation (i18n)
builder.Services.AddSharedLocalization();

// Dependency Injection

// Shared Bounded Context
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Sales Bounded Context
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderCommandService, OrderCommandService>();
builder.Services.AddScoped<IOrderQueryService, OrderQueryService>();

// Profiles Bounded Context
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IProfileCommandService, ProfileCommandService>();
builder.Services.AddScoped<IProfileQueryService, ProfileQueryService>();

// Inventory Bounded Context
builder.Services.AddScoped<IInventoryMaterialRepository, InventoryMaterialRepository>();
builder.Services.AddScoped<IInventoryMaterialCommandService, InventoryMaterialCommandService>();
builder.Services.AddScoped<IInventoryMaterialQueryService, InventoryMaterialQueryService>();

// Engagement Bounded Context
builder.Services.AddScoped<IConversationRepository, ConversationRepository>();
builder.Services.AddScoped<IMessageCommandService, MessageCommandService>();
builder.Services.AddScoped<IMessageQueryService, MessageQueryService>();
builder.Services.AddScoped<INotificationService, NoOpNotificationService>();

// Manufacturing Bounded Context
builder.Services.AddScoped<IManufactureOrderRepository, ProductionRepository>();
builder.Services.AddScoped<IProductionCommandService, ProductionCommandService>();
builder.Services.AddScoped<IProductionQueryService, ProductionQueryService>();

var app = builder.Build();

// Apply pending migrations on startup, creating the database if it doesn't exist
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();

    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Global exception handler — must be first in the pipeline
app.UseGlobalExceptionHandler();

app.UseHttpsRedirection();

// Apply request localisation (reads Accept-Language / ?culture query string)
app.UseSharedLocalization();

app.UseAuthorization();

app.MapControllers();

app.Run();
