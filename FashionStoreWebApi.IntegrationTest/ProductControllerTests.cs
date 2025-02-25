using System.Net;
using System.Net.Http.Json;
using FashionStoreWebApi.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace FashionStoreWebApi.IntegrationTest
{
    public class ProductControllerTests : IClassFixture<FashionStoreWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public ProductControllerTests(FashionStoreWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllProducts_ReturnsSuccessStatusCode()
        {
            var response = await _client.GetAsync("/api/Products");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task CreateProduct_ReturnsSuccessStatusCode()
        {
            MultipartFormDataContent content = new();
            content.Add(new StringContent("Test Product"), "Name");
            content.Add(new StringContent("Test Description"), "Description");
            content.Add(new StringContent(9.99M.ToString()), "Price");
            content.Add(new StringContent("1"), "CategoryId");
            content.Add(new StringContent("1"), "BrandId");
            content.Add(new StringContent("100"), "StockQuantity");
            content.Add(new StringContent("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTbfQoB1EGgSM8vuPSaU3FhWHNzaQ2MikxEIQ&s"), "ImageUrl");

            var response = await _client.PostAsync("/api/Products", content);
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetProductById_ReturnsSuccessStatusCode()
        {
            var response = await _client.GetAsync("/api/Products/1");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task UpdateProduct_ReturnsSuccessStatusCode()
        {
            MultipartFormDataContent content = new();
            content.Add(new StringContent("1"), "Id");
            content.Add(new StringContent("Test Update Product"), "Name");
            content.Add(new StringContent("Test Update Description"), "Description");
            content.Add(new StringContent(9.99M.ToString()), "Price");
            content.Add(new StringContent("1"), "CategoryId");
            content.Add(new StringContent("1"), "BrandId");
            content.Add(new StringContent("100"), "StockQuantity");
            content.Add(new StringContent("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTbfQoB1EGgSM8vuPSaU3FhWHNzaQ2MikxEIQ&s"), "ImageUrl");

            var response = await _client.PutAsync("/api/Products", content);
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteProduct_ReturnsSuccessStatusCode()
        {
            var response = await _client.DeleteAsync("/api/Products/3");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}