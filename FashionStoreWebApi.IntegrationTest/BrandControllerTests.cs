using FashionStoreViewModel;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace FashionStoreWebApi.IntegrationTest
{
    public class BrandControllerTests
    {

        public static readonly JsonSerializerOptions jsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        [Fact]
        public async Task SearchBrandWithNoNameParameter()
        {
            var appFactory = new FashionStoreWebApplicationFactory();
            var client = appFactory.CreateClient();
            var response = await client.GetAsync("/api/Brands");
            var brandPaging = await response.Content.ReadFromJsonAsync<PagingData<BrandVm>>();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            brandPaging.Should().NotBeNull();
            brandPaging.Items.Should().HaveCount(3);
            foreach (var brand in brandPaging.Items)
            {
                brand.Id.Should().BeGreaterThan(0);
                brand.Name.Should().NotBeNullOrEmpty();
            }
        }


        [Fact]
        public async Task SearchBrandWithPumaName()
        {
            var appFactory = new FashionStoreWebApplicationFactory();
            var client = appFactory.CreateClient();
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
            var appFactory = new FashionStoreWebApplicationFactory();
            var client = appFactory.CreateClient();
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
            var appFactory = new FashionStoreWebApplicationFactory();
            var client = appFactory.CreateClient();
            var response = await client.GetAsync("/api/Brands/100");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
