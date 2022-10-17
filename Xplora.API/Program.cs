using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using XploraAPI.PuntosDeVenta.Domain.Repositories;
using XploraAPI.PuntosDeVenta.Domain.Services;
using XploraAPI.PuntosDeVenta.Persistence.Repositories;
using XploraAPI.PuntosDeVenta.Services;
using XploraAPI.Security.Authorization.Handlers.Implementations;
using XploraAPI.Security.Authorization.Handlers.Interfaces;
using XploraAPI.Security.Authorization.Middleware;
using XploraAPI.Security.Authorization.Settings;
using XploraAPI.Security.Domain.Repositories;
using XploraAPI.Security.Domain.Services;
using XploraAPI.Security.Persistence.Repositories;
using XploraAPI.Security.Services;
using XploraAPI.Shared.Domain.Repositories;
using XploraAPI.Shared.Persistence.Contexts;
using XploraAPI.Shared.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Add CORS Service
builder.Services.AddCors(options =>
{
    options.AddPolicy(name:"socialFilmCors", builder =>
    {
        builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddSwaggerGen(options =>
{
    //Add API Documentation Information
    options.SwaggerDoc("v1", new OpenApiInfo());
    options.EnableAnnotations();
    options.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer Scheme."
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference{Type = ReferenceType.SecurityScheme, Id = "bearerAuth" }
            },
            Array.Empty<string>()
        }
    });
});

// ADD DATABASE CONNECTION
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseMySQL(connectionString)
        .LogTo(Console.WriteLine,LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors());

// ADD LOWERCASE ROUTES
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// DEPENDENCY INJECTION CONFIGURATION
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IJwtHandler, JwtHandler>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IPDVRepository, PDVRepository>();
builder.Services.AddScoped<IPDVService, PDVService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();


//AUTOMAPPER CONFIGURATIONDbSet<>Product Prioducts{get;set;}
builder.Services.AddAutoMapper(
    typeof(XploraAPI.PuntosDeVenta.Mapping.ModelToResourceProfile),
    typeof(XploraAPI.PuntosDeVenta.Mapping.ResourceToModelProfile),
    
    typeof(XploraAPI.Security.Mapping.ModelToResourceProfile),
    typeof(XploraAPI.Security.Mapping.ResourceToModelProfile));

builder.Services.AddSwaggerGen();

var app = builder.Build();

//VALIDATION FOR ENSURING DATABASE OBJECTS ARE CREATED
using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<AppDbContext>())
{
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("socialFilmCors");
// Configure Error Handler Middleware
app.UseMiddleware<ErrorHandlerMiddleware>();

// Configure JSON Web Token Handling Middleware
app.UseMiddleware<JwtMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();