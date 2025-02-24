using FashionStoreWebApi.Data;
using FashionStoreWebApi.Identity;
using FashionStoreWebApi.Models;
using FashionStoreWebApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<FashionStoreDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


builder.Services.AddIdentityApiEndpoints<User>()
    .AddRoles<Role>()
    .AddEntityFrameworkStores<FashionStoreDbContext>();

builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IFileStorageService, FileStorageService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddAuthorization();

builder.Services.AddCors(option => option.AddPolicy("wasm",
    policy => policy.WithOrigins(builder.Configuration["BackendUrl"] ?? "",
    builder.Configuration["FrontendUrl"] ?? "")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
));

var app = builder.Build();

app.UseCors("wasm");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(option => option.SwaggerEndpoint("/openapi/v1.json", "Fashion Store Web API"));
    app.MapSwagger().RequireAuthorization();
}

app.UseStaticFiles();

app.MapCustomIdentityApi<User>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
