using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProsjektOppgaveWebAPI.Controllers;
using ProsjektOppgaveWebAPI.Models;
using ProsjektOppgaveWebAPI.Services;

namespace ProsjektOppgaveWebAPITest;

public class BlogControllerTest
{
    private readonly Mock<IBlogService> _mockService;
    private readonly BlogController _controller;

    public BlogControllerTest()
    {
        _mockService = new Mock<IBlogService>();
        _controller = new BlogController(_mockService.Object);
    }
    
    private static IEnumerable<Blog> GetTestBlogs()
    {
        return new List<Blog>
        {
            new Blog(),
            new Blog(),
            new Blog()
        };
    }
    
    
    
    // GET
    [Fact]
    public async Task GetAll_ReturnsCorrectType()
    {
        // Arrange
        _mockService.Setup(service => service.GetAllBlogs())
            .Returns(Task.FromResult(GetTestBlogs()));

        // Act
        var result = await _controller.GetAll();

        // Assert
        Assert.IsType<List<Blog>>(result);
    }

    [Fact]
    public async Task GetAll_ReturnsCorrectNumberOfBlogs()
    {
        // Arrange
        _mockService.Setup(service => service.GetAllBlogs())
            .Returns(Task.FromResult(GetTestBlogs()));

        // Act
        var result = await _controller.GetAll();

        // Assert
        Assert.Equal(3, result.Count());
    }
    
    [Fact]
    public void Get_ReturnsBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        _controller.ModelState.AddModelError("error", "some error");

        // Act
        var result = _controller.Get(1);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }
    
    [Fact]
    public void Get_ReturnsNotFound_WhenIdIsInvalid()
    {
        // Arrange
        _mockService.Setup(service => service.GetBlog(It.IsAny<int>()))
            .Returns((Blog)null);

        // Act
        var result = _controller.Get(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
    
    [Fact]
    public void Get_ReturnsOk_WhenIdIsValid()
    {
        // Arrange
        _mockService.Setup(service => service.GetBlog(It.IsAny<int>()))
            .Returns(new Blog());

        // Act
        var result = _controller.Get(1);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    
    
    // POST
    [Fact]
    public async Task Create_ReturnsBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        _controller.ModelState.AddModelError("error", "some error");

        // Act
        var result = await _controller.Create(new Blog());

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Create_ReturnsCreatedAtAction_WhenSuccessful()
    {
        // Arrange
        var blog = new Blog { BlogId = 1 };
        _mockService.Setup(service => service.Save(blog, It.IsAny<ClaimsPrincipal>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Create(blog);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal("Get", createdAtActionResult.ActionName);
        Assert.Equal(blog, createdAtActionResult.Value);
    }

    
    
    // PUT
    [Fact]
    public async Task Update_ReturnsBadRequest_WhenIdDoesNotMatchBlogId()
    {
        // Arrange
        var blog = new Blog { BlogId = 1 };

        // Act
        var result = await _controller.Update(2, blog);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task Update_ReturnsNotFound_WhenBlogDoesNotExist()
    {
        // Arrange
        var blog = new Blog { BlogId = 1 };
        _mockService.Setup(service => service.GetBlog(It.IsAny<int>()))
            .Returns((Blog)null);

        // Act
        var result = await _controller.Update(1, blog);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Update_ReturnsUnauthorized_WhenUserIsNotOwner()
    {
        // Arrange
        const string userId = "otherUserId";
        var blog = new Blog { BlogId = 1, Owner = new IdentityUser { Id = userId, UserName = "otherUser" } };
        _mockService.Setup(service => service.GetBlog(It.IsAny<int>()))
            .Returns(blog);
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext 
            { 
                User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.NameIdentifier, "testUserId")
                })) 
            }
        };

        // Act
        var result = await _controller.Update(1, blog);

        // Assert
        Assert.IsType<UnauthorizedResult>(result);
    }


    [Fact]
    public async Task Update_ReturnsNoContent_WhenSuccessful()
    {
        // Arrange
        var blog = new Blog { BlogId = 1, Owner = new IdentityUser { UserName = "testUser" } };
        _mockService.Setup(service => service.GetBlog(It.IsAny<int>()))
            .Returns(blog);
        _mockService.Setup(service => service.Save(blog, It.IsAny<ClaimsPrincipal>()))
            .Returns(Task.CompletedTask);
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext 
            { 
                User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, "testUser")
                })) 
            }
        };

        // Act
        var result = await _controller.Update(1, blog);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
    
    
    
    // DELETE
    [Fact]
    public void Delete_ReturnsNotFound_WhenBlogDoesNotExist()
    {
        // Arrange
        _mockService.Setup(service => service.GetBlog(It.IsAny<int>()))
            .Returns((Blog)null);

        // Act
        var result = _controller.Delete(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Delete_ReturnsUnauthorized_WhenUserIsNotOwner()
    {
        // Arrange
        var blog = new Blog { BlogId = 1, OwnerId = "otherUserId" };
        _mockService.Setup(service => service.GetBlog(It.IsAny<int>()))
            .Returns(blog);
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext 
            { 
                User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.NameIdentifier, "testUserId")
                })) 
            }
        };

        // Act
        var result = _controller.Delete(1);

        // Assert
        Assert.IsType<UnauthorizedResult>(result);
    }

    [Fact]
    public void Delete_ReturnsNoContent_WhenSuccessful()
    {
        // Arrange
        var blog = new Blog { BlogId = 1, OwnerId = "testUserId" };
        _mockService.Setup(service => service.GetBlog(It.IsAny<int>()))
            .Returns(blog);
        _mockService.Setup(service => service.Delete(It.IsAny<int>(), It.IsAny<ClaimsPrincipal>()))
            .Returns(Task.CompletedTask);
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext 
            { 
                User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.NameIdentifier, "testUserId")
                })) 
            }
        };

        // Act
        var result = _controller.Delete(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}