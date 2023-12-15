using Microsoft.AspNetCore.Mvc;
using Moq;
using ProsjektOppgaveWebAPI.Controllers;
using ProsjektOppgaveWebAPI.Services.CommentServices;
using ProsjektOppgaveWebAPI.Services.CommentServices.Models;
using Microsoft.AspNetCore.SignalR;
using ProsjektOppgaveWebAPI.Database.Entities;
using ProsjektOppgaveWebAPI.Hubs;
using ProsjektOppgaveWebAPI.Services.Response;

namespace ProsjektOppgaveWebAPITest;

public class CommentControllerTest
{
    private readonly Mock<ICommentService> _serviceMock;
    private readonly CommentController _controller;

    public CommentControllerTest()
    {
        _serviceMock = new Mock<ICommentService>();
        _controller = new CommentController(_serviceMock.Object, new Mock<IHubContext<CommentHub>>().Object);
    }

    [Fact]
    public async Task GetComments_ReturnsExpectedComments()
    {
        // Arrange
        const int postId = 1;
        var expectedComments = new List<Comment> { new Comment(), new Comment() };
        _serviceMock.Setup(service => service.GetCommentsForPost(postId)).ReturnsAsync(expectedComments);

        // Act
        var result = await _controller.GetComments(postId);

        // Assert
        Assert.Equal(expectedComments, result);
    }

    [Fact]
    public void GetComment_ReturnsExpectedComment()
    {
        // Arrange
        const int commentId = 1;
        var expectedComment = new Comment();
        _serviceMock.Setup(service => service.GetComment(commentId)).Returns(expectedComment);

        // Act
        var result = _controller.GetComment(commentId);

        // Assert
        Assert.Equal(expectedComment, result);
    }

    [Fact]
    public async Task Create_ReturnsBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        _controller.ModelState.AddModelError("error", "some error");
        var commentModel = new CreateCommentHttpPostModel();

        // Act
        var result = await _controller.Create(commentModel);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Create_ReturnsOk_WhenSuccessful()
    {
        // Arrange
        var commentModel = new CreateCommentHttpPostModel();
        _serviceMock.Setup(service => service.Save(commentModel))
            .ReturnsAsync(ResponseService<int>.Ok(1));

        // Act
        var result = await _controller.Create(commentModel);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(1, okResult.Value);
    }

    [Fact]
    public async Task Update_ReturnsBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        _controller.ModelState.AddModelError("error", "some error");
        var updateModel = new UpdateCommentHttpPostModel();

        // Act
        var result = await _controller.Update(updateModel);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Update_ReturnsOk_WhenSuccessful()
    {
        // Arrange
        var updateModel = new UpdateCommentHttpPostModel();
        _serviceMock.Setup(service => service.Update(updateModel))
            .ReturnsAsync(ResponseService<Comment>.Ok(new Comment()));

        // Act
        var result = await _controller.Update(updateModel);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsBadRequest_WhenCommentDoesNotExist()
    {
        // Arrange
        const int commentId = 1;
        _serviceMock.Setup(service => service.Delete(commentId))
            .ReturnsAsync(ResponseService<bool>.Error("Error Message"));

        // Act
        var result = await _controller.Delete(commentId);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsOk_WhenSuccessful()
    {
        // Arrange
        const int commentId = 1;
        _serviceMock.Setup(service => service.Delete(commentId))
            .ReturnsAsync(ResponseService<bool>.Ok(true));

        // Act
        var result = await _controller.Delete(commentId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(commentId, okResult.Value);
    }
}
