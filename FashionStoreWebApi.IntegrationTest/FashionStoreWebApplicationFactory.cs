using System.Data.Common;
using FashionStoreWebApi.Data;
using FashionStoreWebApi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace FashionStoreWebApi.IntegrationTest
{
    public class FashionStoreWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                

                //var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Database=FashionStoreDBTest;Trusted_Connection=True;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

                //services.AddDbContext<FashionStoreDbContext>(options =>
                //{
                //    options.UseSqlServer(connectionString);
                //});

                var dbContextDescriptor = services.SingleOrDefault(
                      d => d.ServiceType ==
                      typeof(IDbContextOptionsConfiguration<FashionStoreDbContext>));

                services.Remove(dbContextDescriptor);

                services.AddDbContext<FashionStoreDbContext>((options, context) =>
                {
                    context.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<FashionStoreDbContext>();
                    db.Database.EnsureDeleted();
                    db.Database.EnsureCreated();
                    //db.Database.Migrate();
                    SeedingDataHelper.InitializeDB(db);
                }

                services.AddAuthentication("TestSchema")
                    .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                        "TestSchema", options => { });

            });

            builder.UseEnvironment("Development");

        }
    }
}
