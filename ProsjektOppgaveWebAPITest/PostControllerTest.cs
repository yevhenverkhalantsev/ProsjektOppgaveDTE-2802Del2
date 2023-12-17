using Microsoft.AspNetCore.Mvc;
using Moq;
using ProsjektOppgaveWebAPI.Controllers;
using ProsjektOppgaveWebAPI.Database.Entities;
using ProsjektOppgaveWebAPI.Services.BlogServices;
using ProsjektOppgaveWebAPI.Services.PostServices;
using ProsjektOppgaveWebAPI.Services.PostServices.Models;
using ProsjektOppgaveWebAPI.Services.Response;

namespace ProsjektOppgaveWebAPITest;

public class PostControllerTest
{
    private readonly Mock<IPostService> _postServiceMock;
    private readonly PostController _controller;

    public PostControllerTest()
    {
        _postServiceMock = new Mock<IPostService>();
        _controller = new PostController( _postServiceMock.Object, null);
    }

    [Fact]
    public async Task GetPosts_ReturnsExpectedPosts()
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
    public async Task GetPost_ReturnsOkWithExpectedPost()
    {
        // Arrange
        var postId = 1;
        var expectedPost = new Post();
        _postServiceMock.Setup(service => service.GetPost(postId)).ReturnsAsync(ResponseService<Post>.Ok(expectedPost));

        // Act
        var result = await _controller.GetPost(postId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(expectedPost, okResult.Value);
    }

    [Fact]
    public async Task Create_ReturnsBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        _controller.ModelState.AddModelError("error", "some error");
        var createModel = new CreatePostHttpPostModel();

        // Act
        var result = await _controller.Create(createModel);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Create_ReturnsOk_WhenSuccessful()
    {
        // Arrange
        var createModel = new CreatePostHttpPostModel();
        _postServiceMock.Setup(service => service.SavePost(createModel))
            .ReturnsAsync(ResponseService<int>.Ok(1));

        // Act
        var result = await _controller.Create(createModel);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(1, okResult.Value);
    }
}
