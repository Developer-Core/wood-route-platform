using DeveloperCore.WoodRoute.Platform.Customers.Application.Acl;
using DeveloperCore.WoodRoute.Platform.Customers.Application.CommandServices;
using DeveloperCore.WoodRoute.Platform.Customers.Application.Internal.CommandServices;
using DeveloperCore.WoodRoute.Platform.Customers.Application.Internal.QueryServices;
using DeveloperCore.WoodRoute.Platform.Customers.Application.QueryServices;
using DeveloperCore.WoodRoute.Platform.Customers.Domain.Repositories;
using DeveloperCore.WoodRoute.Platform.Customers.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using DeveloperCore.WoodRoute.Platform.Customers.Interfaces.Acl;
using DeveloperCore.WoodRoute.Platform.Engagement.Application.CommandServices;
using DeveloperCore.WoodRoute.Platform.Engagement.Application.Internal.CommandServices;
using DeveloperCore.WoodRoute.Platform.Engagement.Application.Internal.QueryServices;
using DeveloperCore.WoodRoute.Platform.Engagement.Application.OutboundServices;
using DeveloperCore.WoodRoute.Platform.Engagement.Application.QueryServices;
using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Repositories;
using DeveloperCore.WoodRoute.Platform.Engagement.Infrastructure.OutboundServices;
using DeveloperCore.WoodRoute.Platform.Engagement.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using DeveloperCore.WoodRoute.Platform.Inventory.Application.CommandServices;
using DeveloperCore.WoodRoute.Platform.Inventory.Application.Internal.CommandServices;
using DeveloperCore.WoodRoute.Platform.Inventory.Application.Internal.QueryServices;
using DeveloperCore.WoodRoute.Platform.Inventory.Application.QueryServices;
using DeveloperCore.WoodRoute.Platform.Inventory.Domain.Repositories;
using DeveloperCore.WoodRoute.Platform.Inventory.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Application.Acl;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Application.CommandServices;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Application.Internal.CommandServices;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Application.Internal.QueryServices;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Application.QueryServices;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Repositories;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Interfaces.Acl;
using DeveloperCore.WoodRoute.Platform.Sales.Application.Acl;
using DeveloperCore.WoodRoute.Platform.Sales.Application.CommandServices;
using DeveloperCore.WoodRoute.Platform.Sales.Application.Internal.CommandServices;
using DeveloperCore.WoodRoute.Platform.Sales.Application.Internal.QueryServices;
using DeveloperCore.WoodRoute.Platform.Sales.Application.QueryServices;
using DeveloperCore.WoodRoute.Platform.Sales.Domain.Repositories;
using DeveloperCore.WoodRoute.Platform.Sales.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using DeveloperCore.WoodRoute.Platform.Engagement.Application.Internal.EventHandlers;
using DeveloperCore.WoodRoute.Platform.Iam.Application.Acl;
using DeveloperCore.WoodRoute.Platform.Iam.Application.CommandServices;
using DeveloperCore.WoodRoute.Platform.Iam.Application.Internal.CommandServices;
using DeveloperCore.WoodRoute.Platform.Iam.Application.Internal.OutboundServices;
using DeveloperCore.WoodRoute.Platform.Iam.Application.Internal.QueryServices;
using DeveloperCore.WoodRoute.Platform.Iam.Application.QueryServices;
using DeveloperCore.WoodRoute.Platform.Iam.Domain.Repositories;
using DeveloperCore.WoodRoute.Platform.Iam.Infrastructure.Hashing.BCrypt.Services;
using DeveloperCore.WoodRoute.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using DeveloperCore.WoodRoute.Platform.Iam.Infrastructure.Pipeline.Middleware.Extensions;
using DeveloperCore.WoodRoute.Platform.Iam.Infrastructure.Registration.Configuration;
using DeveloperCore.WoodRoute.Platform.Iam.Infrastructure.Tokens.Jwt.Configuration;
using DeveloperCore.WoodRoute.Platform.Iam.Infrastructure.Tokens.Jwt.Services;
using DeveloperCore.WoodRoute.Platform.Iam.Interfaces.Acl;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Events;
using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Events;
using DeveloperCore.WoodRoute.Platform.Sales.Interfaces.Acl;
using DeveloperCore.WoodRoute.Platform.Shared.Application.Internal.EventHandlers;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Events;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Repositories;
using DeveloperCore.WoodRoute.Platform.Shared.Infrastructure.EventHandlers;
using DeveloperCore.WoodRoute.Platform.Shared.Infrastructure.Interfaces.AspNetCore.Configuration;
using DeveloperCore.WoodRoute.Platform.Shared.Infrastructure.Interfaces.AspNetCore.Configuration.Extensions;
using DeveloperCore.WoodRoute.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using DeveloperCore.WoodRoute.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using DeveloperCore.WoodRoute.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

// CORS — origins are read from configuration so each environment declares its own
// allowed clients (Vite dev server locally, the Vercel domain in production).
// In production Render injects them via the env var Cors__AllowedOrigins__0=https://<vercel-domain>.
const string WoodRouteClientsPolicy = "AllowWoodRouteClients";
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];
builder.Services.AddCors(options =>
{
    options.AddPolicy(WoodRouteClientsPolicy, policy =>
        policy.WithOrigins(allowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// Add Database Connection
var connectionStringTemplate = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(connectionStringTemplate))
{
    throw new InvalidOperationException("Connection string not found.");
}

var connectionString = Environment.ExpandEnvironmentVariables(connectionStringTemplate);

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
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "DeveloperCore.WoodRoute.Platform",
            Version = "v1",
            Description = "DeveloperCore WoodRoute Platform API for custom furniture orders, manufacturing and inventory."
        });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
        { [new OpenApiSecuritySchemeReference("Bearer", document)] = [] });
    options.EnableAnnotations();
});

// Internationalisation (i18n)
builder.Services.AddSharedLocalization();

// Token settings bound from the "TokenSettings" section of appsettings.json
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

// Carpenter settings bound from the "Carpenter" section of appsettings.json — gates the closed
// carpenter registration flow with an invitation code.
builder.Services.Configure<CarpenterSettings>(builder.Configuration.GetSection("Carpenter"));

// Dependency Injection

// Shared Bounded Context
builder.Services.AddScoped<ProblemDetailsFactory>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

// Domain Event Handlers
builder.Services.AddScoped<IDomainEventHandler<StageUpdatedEvent>, StageUpdatedEventHandler>();
builder.Services.AddScoped<IDomainEventHandler<OrderCreatedEvent>, OrderCreatedEventHandler>();

// Sales Bounded Context
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderCommandService, OrderCommandService>();
builder.Services.AddScoped<IOrderQueryService, OrderQueryService>();
builder.Services.AddScoped<ISalesContextFacade, SalesContextFacade>();

// Customers Bounded Context
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerCommandService, CustomerCommandService>();
builder.Services.AddScoped<ICustomerQueryService, CustomerQueryService>();
builder.Services.AddScoped<ICustomersContextFacade, CustomersContextFacade>();

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
builder.Services.AddScoped<IManufactureOrderRepository, ManufactureOrderRepository>();
builder.Services.AddScoped<IProductionCommandService, ProductionCommandService>();
builder.Services.AddScoped<IProductionQueryService, ProductionQueryService>();
builder.Services.AddScoped<IManufacturingContextFacade, ManufacturingContextFacade>();

// Iam Bounded Context
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();

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

// HTTPS redirection is skipped in Development so browser CORS preflight (OPTIONS) requests
// are not answered with a 307 to https://localhost:7155 — a redirected preflight fails the
// whole CORS handshake. In production TLS is terminated at the Render proxy.
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

// Apply request localisation (reads Accept-Language / ?culture query string)
app.UseSharedLocalization();

app.UseRouting();

// CORS must run after routing and before the authorization middleware, otherwise the
// anonymous preflight OPTIONS request would be rejected by UseRequestAuthorization.
app.UseCors(WoodRouteClientsPolicy);

// Protect all endpoints by default; only [AllowAnonymous] endpoints skip token validation
app.UseRequestAuthorization();

app.UseAuthorization();

app.MapControllers();

app.Run();
