using System.Net;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Headers;
using System.Net.Http.Json;
namespace projet.Tests
{public class UserServiceTests :  IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly IServiceProvider _serviceProvider;
    private readonly CustomWebApplicationFactory<Program> _factory;
    public UserServiceTests(CustomWebApplicationFactory<Program> factory)
    {
         _factory = factory;
        _client = factory.CreateClient();
        _serviceProvider = factory.Services;
    }

    [Fact]
    public async Task GetAllUsers_ShouldReturnAllUsers()
    {
        // Arrange
        var service = _serviceProvider.GetRequiredService<IUserService>();

        // Act
        var users = await service.GetAllUsersWithRolesAsync();

        // Assert
        Assert.NotNull(users);
        Assert.True(users.Any());
    }

    [Fact]
    public async Task CreateUser_ShouldAddUserToDatabase()
    {
        // Arrange
        var user = new User
        {
            Username = "testuser",
            Email = "test@example.com",
            Password = "Password123"
        };

        var service = _serviceProvider.GetRequiredService<IUserService>();

        // Act
        var createdUser = await service.CreateUserAsync(user);

        // Assert
        Assert.NotNull(createdUser);
        Assert.Equal(user.Username, createdUser.Username);
        Assert.Equal(user.Email, createdUser.Email);
    }
}

}
