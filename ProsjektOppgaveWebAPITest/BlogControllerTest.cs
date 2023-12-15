using Microsoft.AspNetCore.Mvc;
using Moq;
using ProsjektOppgaveWebAPI.Controllers;
using ProsjektOppgaveWebAPI.Services.BlogServices;
using ProsjektOppgaveWebAPI.Database.Entities;
using ProsjektOppgaveWebAPI.Models.Blog;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using ProsjektOppgaveWebAPI.Hubs;
using ProsjektOppgaveWebAPI.Services.Response;

namespace ProsjektOppgaveWebAPITest;

public class BlogControllerTest
{
    private readonly Mock<IBlogService> _mockService;
    private readonly Mock<IHubContext<BlogHub>> _mockHubContext;
    private readonly BlogController _controller;

    public BlogControllerTest()
    {
        _mockService = new Mock<IBlogService>();
        _mockHubContext = new Mock<IHubContext<BlogHub>>();
        _controller = new BlogController(_mockService.Object, _mockHubContext.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkWithSerializedBlogs()
    {
        // Arrange
        var mockBlogs = new List<Blog> { new Blog(), new Blog() };
        _mockService.Setup(service => service.GetAllBlogs())
            .ReturnsAsync(mockBlogs);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(JsonConvert.SerializeObject(mockBlogs), okResult.Value);
    }

    [Fact]
    public async Task Get_ReturnsOkWithBlogViewModel()
    {
        // Arrange
        var blogId = 1;
        var mockBlog = new Blog { BlogId = blogId, Name = "Test Blog", Posts = new List<Post>() };
        _mockService.Setup(service => service.GetBlog(blogId))
            .ReturnsAsync(ResponseService<Blog>.Ok(mockBlog));

        // Act
        var result = await _controller.Get(blogId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var viewModel = Assert.IsType<BlogViewModel>(okResult.Value);
        Assert.Equal("Test Blog", viewModel.Title);
    }

    [Fact]
    public async Task Get_ReturnsNotFoundWhenBlogNotExists()
    {
        // Arrange
        _mockService.Setup(service => service.GetBlog(It.IsAny<int>()))
            .ReturnsAsync(ResponseService<Blog>.Error("Not Found"));

        // Act
        var result = await _controller.Get(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Get_ReturnsNotFoundWhenBlogIsNull()
    {
        // Arrange
        _mockService.Setup(service => service.GetBlog(It.IsAny<int>()))
            .ReturnsAsync(ResponseService<Blog>.Ok(null));

        // Act
        var result = await _controller.Get(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
        
    [Fact]
    public async Task Get_ReturnsNotFoundWhenBlogIsError()
    {
        // Arrange
        _mockService.Setup(service => service.GetBlog(It.IsAny<int>()))
            .ReturnsAsync(ResponseService<Blog>.Error("Not Found"));

        // Act
        var result = await _controller.Get(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
        
    [Fact]
    public async Task Get_ReturnsNotFoundWhenBlogIsErrorAndValueIsNull()
    {
        // Arrange
        _mockService.Setup(service => service.GetBlog(It.IsAny<int>()))
            .ReturnsAsync(ResponseService<Blog>.Error(null));

        // Act
        var result = await _controller.Get(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}