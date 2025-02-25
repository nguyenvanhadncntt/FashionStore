using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using FashionStoreViewModel;
using FluentAssertions;

namespace FashionStoreWebApi.IntegrationTest
{
    public class RoleControllerTests : IClassFixture<FashionStoreWebApplicationFactory>
    {
        private readonly HttpClient client;
        public RoleControllerTests(FashionStoreWebApplicationFactory factory)
        {
            client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllRoles_Success()
        {
            var response = await client.GetAsync("/api/Roles");
            var roles = await response.Content.ReadFromJsonAsync<List<RoleVm>>();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            roles.Should().HaveCount(2);
            foreach (var role in roles)
            {
                role.Id.Should().NotBeEmpty();
                role.Name.Should().NotBeNullOrEmpty();
            }
        }

        [Fact]
        public async Task GetRoleById_Success()
        {
            var roleId = "f53d4d27-1c1f-47ee-9e72-0aaf95535b2a";
            var response = await client.GetAsync($"/api/Roles/{roleId}");
            var role = await response.Content.ReadFromJsonAsync<RoleVm>();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            role.Id.Should().Be(roleId);
            role.Name.Should().Be("Admin");
        }

        [Fact]
        public async Task GetRoleById_NotFound()
        {
            var response = await client.GetAsync($"/api/Roles/1");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task AddRole_Success()
        {
            var role = new RoleVm
            {
                Name = "Super Admin"
            };
            var response = await client.PostAsJsonAsync("/api/Roles", role);
            var addedRole = await response.Content.ReadFromJsonAsync<RoleVm>();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            addedRole.Id.Should().NotBeEmpty();
            addedRole.Name.Should().Be("Super Admin");
        }

        [Fact]
        public async Task AddRole_RoleAlready_ExistingException()
        {
            var role = new RoleVm
            {
                Name = "Admin"
            };
            var response = await client.PostAsJsonAsync("/api/Roles", role);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateRole_Success()
        {
            var role = new RoleVm
            {
                Id = Guid.Parse("f53d4d27-1c1f-47ee-9e72-0aaf95535b2a"),
                Name = "Super Admin"
            };
            var response = await client.PutAsJsonAsync("/api/Roles", role);
            var updatedRole = await response.Content.ReadFromJsonAsync<RoleVm>();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            updatedRole.Id.Should().Be(role.Id);
            updatedRole.Name.Should().Be("Super Admin");
        }

        [Fact]
        public async Task UpdateRole_RoleNotFound()
        {
            var role = new RoleVm
            {
                Id = Guid.Parse("f53d4d27-1c1f-47ee-9e72-0aaf95535b2c"),
                Name = "Super Admin"
            };
            var response = await client.PutAsJsonAsync("/api/Roles", role);
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task DeleteRole_Success()
        {
            var roleId = "f53d4d27-1c1f-47ee-9e72-0aaf95535b2a";
            var response = await client.DeleteAsync($"/api/Roles/{roleId}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
