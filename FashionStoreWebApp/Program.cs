using Blazored.Toast;
using FashionStoreWebApp;
using FashionStoreWebApp.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredToast();
builder.Services.AddAuthorizationCore();
builder.Services.AddTransient<CutomHttpHandler>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped(sp => (IAccountManagementService)sp.GetRequiredService<AuthenticationStateProvider>());

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress =
    new Uri(builder.Configuration["FrontendUrl"] ?? "https://localhost:7083")
});

builder.Services.AddHttpClient("Auth", opt => opt.BaseAddress =
new Uri(builder.Configuration["BackendUrl"] ?? "https://localhost:7012"))
    .AddHttpMessageHandler<CutomHttpHandler>();

await builder.Build().RunAsync();
