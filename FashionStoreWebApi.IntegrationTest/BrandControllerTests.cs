using FashionStoreViewModel;
using FashionStoreWebApi.Data;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace FashionStoreWebApi.IntegrationTest
{
    public class BrandControllerTests : IClassFixture<FashionStoreWebApplicationFactory>
    {

        private readonly HttpClient client;
        public BrandControllerTests(FashionStoreWebApplicationFactory factory)
        {
            client = factory.CreateClient();
        }

        public static readonly JsonSerializerOptions jsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        [Fact]
        public async Task SearchBrandWithNoNameParameter()
        {
            var response = await client.GetAsync("/api/Brands");
            var brandPaging = await response.Content.ReadFromJsonAsync<PagingData<BrandVm>>();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            brandPaging.Should().NotBeNull();
            foreach (var brand in brandPaging.Items)
            {
                brand.Id.Should().BeGreaterThan(0);
                brand.Name.Should().NotBeNullOrEmpty();
            }
        }


        [Fact]
        public async Task SearchBrandWithPumaName()
        {
            var response = await client.GetAsync("/api/Brands?name=Puma");
            var brandPaging = await response.Content.ReadFromJsonAsync<PagingData<BrandVm>>();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            brandPaging.Should().NotBeNull();
            brandPaging.Items.Should().HaveCount(1);
            foreach (var brand in brandPaging.Items)
            {
                brand.Id.Should().Be(3);
                brand.Name.Should().Be("Puma");
                brand.Description.Should().Be("Forever Faster");
            }
        }

        [Fact]
        public async Task GetBrandById_Success()
        {
            var response = await client.GetAsync("/api/Brands/2");
            var brand = await response.Content.ReadFromJsonAsync<BrandVm>();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            brand.Should().NotBeNull();
            brand.Id.Should().Be(2);
            brand.Name.Should().Be("Adidas");
            brand.Description.Should().Be("Impossible is Nothing");
        }

        [Fact]
        public async Task GetBrandById_NotFound()
        {
            var response = await client.GetAsync("/api/Brands/100");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task CreateBrand_Success()
        {
            var brand = new BrandVm()
            {
                Name = "Dior",
                Description = "Dior, the essence of style"
            };
            var response = await client.PostAsJsonAsync("/api/Brands", brand);
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var brandResponse = await response.Content.ReadFromJsonAsync<BrandVm>();
            brandResponse.Id.Should().Be(4);
        }

        [Fact]
        public async Task UpdateBrand_Success()
        {
            var brand = new BrandVm()
            {
                Id = 1,
                Name = "Adidas",
                Description = "Adidas update"
            };
            var response = await client.PutAsJsonAsync("/api/Brands", brand);
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task UpdateBrand_NotFound_Brand()
        {
            var brand = new BrandVm()
            {
                Id = 100,
                Name = "Not Exist Brand",
                Description = "Not Exist Brand"
            };
            var response = await client.PutAsJsonAsync("/api/Brands", brand);
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task DeleteBrand_Success()
        {
            var response = await client.DeleteAsync("/api/Brands/1");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
