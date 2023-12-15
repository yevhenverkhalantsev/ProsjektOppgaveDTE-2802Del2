using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using ProsjektOppgaveWebAPI.Controllers;
using ProsjektOppgaveWebAPI.Models.ViewModel;

namespace ProsjektOppgaveWebAPITest;

public class AuthControllerTest
{
    private readonly Mock<UserManager<IdentityUser>> _userManagerMock;
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly AuthController _controller;

    public AuthControllerTest()
    {
        _userManagerMock = new Mock<UserManager<IdentityUser>>(
            Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);
        _configurationMock = new Mock<IConfiguration>();

        _controller = new AuthController(_userManagerMock.Object, _configurationMock.Object);
    }

    [Fact]
    public async Task Login_ReturnsOk_WhenValidCredentials()
    {
        // Arrange
        var loginViewModel = new LoginViewModel { Username = "testuser", Password = "password" };
        var identityUser = new IdentityUser { UserName = loginViewModel.Username };
        _userManagerMock.Setup(x => x.FindByNameAsync(loginViewModel.Username)).ReturnsAsync(identityUser);
        _userManagerMock.Setup(x => x.CheckPasswordAsync(identityUser, loginViewModel.Password)).ReturnsAsync(true);

        // Act
        var result = await _controller.Login(loginViewModel);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Login_ReturnsUnauthorized_WhenInvalidCredentials()
    {
        // Arrange
        var loginViewModel = new LoginViewModel { Username = "testuser", Password = "wrongpassword" };
        var identityUser = new IdentityUser { UserName = loginViewModel.Username };
        _userManagerMock.Setup(x => x.FindByNameAsync(loginViewModel.Username)).ReturnsAsync(identityUser);
        _userManagerMock.Setup(x => x.CheckPasswordAsync(identityUser, loginViewModel.Password)).ReturnsAsync(false);

        // Act
        var result = await _controller.Login(loginViewModel);

        // Assert
        Assert.IsType<UnauthorizedResult>(result);
    }

    [Fact]
    public async Task Register_ReturnsOk_WhenNewUser()
    {
        // Arrange
        var registerViewModel = new RegisterViewModel { Username = "newuser", Email = "newuser@example.com", Password = "password" };
        _userManagerMock.Setup(x => x.FindByNameAsync(registerViewModel.Username)).ReturnsAsync(value: null);
        _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(), registerViewModel.Password)).ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _controller.Register(registerViewModel);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Register_ReturnsInternalServerError_WhenUserExists()
    {
        // Arrange
        var registerViewModel = new RegisterViewModel { Username = "existinguser", Email = "existinguser@example.com", Password = "password" };
        var identityUser = new IdentityUser { UserName = registerViewModel.Username };
        _userManagerMock.Setup(x => x.FindByNameAsync(registerViewModel.Username)).ReturnsAsync(identityUser);

        // Act
        var result = await _controller.Register(registerViewModel);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
    }
}
