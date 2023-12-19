using Microsoft.AspNetCore.Mvc;
using Moq;
using ProsjektOppgaveWebAPI.Controllers;
using ProsjektOppgaveWebAPI.Services.CommentServices;
using ProsjektOppgaveWebAPI.Services.CommentServices.Models;
using Microsoft.AspNetCore.SignalR;
using ProsjektOppgaveWebAPI.Database.Entities;
using ProsjektOppgaveWebAPI.Hubs;
using ProsjektOppgaveWebAPI.Services.Response;
using Xunit;

namespace ProsjektOppgaveWebAPITest
{
    public class CommentControllerTests
    {
        private readonly Mock<ICommentService> _commentServiceMock;
        private readonly CommentController _controller;

        public CommentControllerTests()
        {
            _commentServiceMock = new Mock<ICommentService>();
            _controller = new CommentController(_commentServiceMock.Object, new Mock<IHubContext<CommentHub>>().Object);
        }

        [Fact]
        public async Task GetComments_ShouldReturnCommentsForPost()
        {
            // Arrange
            var postId = 1;
            var expectedComments = new List<Comment> { new Comment(), new Comment() };
            _commentServiceMock.Setup(s => s.GetCommentsForPost(postId)).ReturnsAsync(expectedComments);

            // Act
            var result = await _controller.GetComments(postId);

            // Assert
            Assert.Equal(expectedComments, result);
        }

        [Fact]
        public void GetComment_ShouldReturnSpecificComment()
        {
            // Arrange
            var commentId = 1;
            var expectedComment = new Comment();
            _commentServiceMock.Setup(s => s.GetComment(commentId)).Returns(expectedComment);

            // Act
            var result = _controller.GetComment(commentId);

            // Assert
            Assert.Equal(expectedComment, result);
        }

        [Fact]
        public async Task Create_ShouldReturnBadRequestForInvalidModel()
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
        public async Task Update_ShouldReturnOkForValidModel()
        {
            // Arrange
            var updateModel = new UpdateCommentHttpPostModel();
            _commentServiceMock.Setup(s => s.Update(updateModel))
                .ReturnsAsync(ResponseService<Comment>.Ok(new Comment()));

            // Act
            var result = await _controller.Update(updateModel);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Delete_ShouldReturnBadRequestWhenCommentNotFound()
        {
            // Arrange
            var commentId = 1;
            _commentServiceMock.Setup(s => s.Delete(commentId))
                .ReturnsAsync(ResponseService<bool>.Error("Error Message"));

            // Act
            var result = await _controller.Delete(commentId);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
