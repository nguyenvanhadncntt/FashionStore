using System.Net;
using System.Net.Http.Json;
using FashionStoreViewModel;
using FluentAssertions;

namespace FashionStoreWebApi.IntegrationTest
{
    public class UserControllerTests : IClassFixture<FashionStoreWebApplicationFactory>
    {
        private readonly HttpClient client;

        public UserControllerTests(FashionStoreWebApplicationFactory factory)
        {
            client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllUsers_Success()
        {
            var response = await client.GetAsync("/api/Users");
            var pagingUsers = await response.Content.ReadFromJsonAsync<PagingData<UserVm>>();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            pagingUsers.Should().NotBeNull();
            foreach (var user in pagingUsers.Items)
            {
                user.Id.Should().NotBeEmpty();
                user.Email.Should().NotBeNullOrEmpty();
            }
        }

        [Fact]
        public async Task GetUserById_Success()
        {
            var userId = "4578316a-c937-4fe0-85d2-1d4f488f3a58";
            var response = await client.GetAsync($"/api/Users/{userId}");
            var user = await response.Content.ReadFromJsonAsync<UserVm>();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            user.Id.Should().Be(userId);
            user.Email.Should().Be("admin@gmail.com");
        }

        [Fact]
        public async Task GetUserById_NotFound()
        {
            var userId = "4578316a-c937-4fe0-85d2-1d4f48823123";
            var response = await client.GetAsync($"/api/Users/{userId}");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task CreateUser_Success()
        {
            var user = new UserCreationDTO
            {
                Email = "new_user@gmail.com",
                Password = "NewUser123!",
                FirstName = "New",
                LastName = "User",
                PhoneNumber = "1234567890",
                Role = "User-Policy"
            };
            var response = await client.PostAsJsonAsync("/api/Users", user);
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task CreateUser_NotExist_Role_Exception()
        {
            var user = new UserCreationDTO
            {
                Email = "notExist@gmail.com",
                Password = "NewUser123!",
                FirstName = "notExist",
                LastName = "User",
                PhoneNumber = "1234567890",
                Role = "NotExist"
            };
            var response = await client.PostAsJsonAsync("/api/Users", user);
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdateUser_Success()
        {
            var user = new UserCreationDTO
            {
                Id = "4578316a-c937-4fe0-85d2-1d4f488f3a58",
                Email = "admin@gmail.com",
                Password = "NewUser123!",
                FirstName = "Admin Admin",
                LastName = "Admin",
                PhoneNumber = "1234567890",
                Role = "Admin-Policy"
            };
            var response = await client.PutAsJsonAsync("/api/Users", user);
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task UpdateUser_NotExist_User_Exception()
        {
            var user = new UserCreationDTO
            {
                Id = "4578316a-c937-4fe0-85d2-1d4c48823123",
                Email = "admin@gmail.com",
                Password = "NewUser123!",
                FirstName = "Admin Admin",
                LastName = "Admin",
                PhoneNumber = "1234567890",
                Role = "Admin-Policy"
            };
            var response = await client.PutAsJsonAsync("/api/Users", user);
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdateUser_NotExist_Role_Exception()
        {
            var user = new UserCreationDTO
            {
                Id = "4578316a-c937-4fe0-85d2-1d4f488f3a58",
                Email = "admin@gmail.com",
                Password = "NewUser123!",
                FirstName = "Admin Admin",
                LastName = "Admin",
                PhoneNumber = "1234567890",
                Role = "RoleNotExist"
            };
            var response = await client.PutAsJsonAsync("/api/Users", user);
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task DeleteUser_Success()
        {
            var userId = "7848f387-81a9-4757-91f4-f74e10d69c15";
            var response = await client.DeleteAsync($"/api/Users/{userId}");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
