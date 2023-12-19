using Microsoft.AspNetCore.Mvc;
using Moq;
using ProsjektOppgaveWebAPI.Controllers;
using ProsjektOppgaveWebAPI.Database.Entities;
using ProsjektOppgaveWebAPI.Services.PostServices;
using ProsjektOppgaveWebAPI.Services.PostServices.Models;
using ProsjektOppgaveWebAPI.Services.Response;

namespace ProsjektOppgaveWebAPITest;

public class PostControllerTests
{
    private readonly Mock<IPostService> _postServiceMock;
    private readonly PostController _controller;

    public PostControllerTests()
    {
        _postServiceMock = new Mock<IPostService>();
        _controller = new PostController(_postServiceMock.Object, null);
    }

    [Fact]
    public async Task GetPosts_ShouldReturnListOfPostsForGivenBlogId()
    {
        // Arrange
        const int blogId = 1;
        var expectedPosts = new List<Post> { new Post(), new Post() };
        _postServiceMock.Setup(service => service.GetPostsForBlog(blogId)).ReturnsAsync(expectedPosts);

        // Act
        var result = await _controller.GetPosts(blogId);

        // Assert
        Assert.Equal(expectedPosts, result);
    }

    [Fact]
    public async Task CreatePost_ShouldReturnBadRequestWhenModelIsInvalid()
    {
        // Arrange
        _controller.ModelState.AddModelError("error", "some error");
        var createPostModel = new CreatePostHttpPostModel();

        // Act
        var result = await _controller.Create(createPostModel);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task CreatePost_ShouldReturnOkWithPostIdWhenSuccessful()
    {
        // Arrange
        var createPostModel = new CreatePostHttpPostModel();
        _postServiceMock.Setup(service => service.SavePost(createPostModel))
            .ReturnsAsync(ResponseService<int>.Ok(1));

        // Act
        var result = await _controller.Create(createPostModel);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(1, okResult.Value);
    }
}