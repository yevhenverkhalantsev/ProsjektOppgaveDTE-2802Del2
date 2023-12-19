using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using ProsjektOppgaveWebAPI.Controllers;
using ProsjektOppgaveWebAPI.Hubs;
using ProsjektOppgaveWebAPI.Models.Tag;
using ProsjektOppgaveWebAPI.Services.TagServices;
using ProsjektOppgaveWebAPI.Services.Response;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using ProsjektOppgaveWebAPI.Database.Entities;
using ProsjektOppgaveWebAPI.Services.TagServices.Models;

namespace ProsjektOppgaveWebAPITest
{
    public class TagControllerTests
    {
        private readonly Mock<ITagService> _tagServiceMock;
        private readonly Mock<IHubContext<TagHub>> _hubContextMock;
        private readonly TagController _controller;

        public TagControllerTests()
        {
            _tagServiceMock = new Mock<ITagService>();
            _hubContextMock = new Mock<IHubContext<TagHub>>();
            _controller = new TagController(_tagServiceMock.Object, _hubContextMock.Object);
        }

        [Fact]
        public async Task CreateTag_ReturnsOk_WhenCreationIsSuccessful()
        {
            // Arrange
            var createTagModel = new CreateTagHttpPostModel();
            _tagServiceMock.Setup(service => service.Create(createTagModel))
                           .ReturnsAsync(ResponseService<Tag>.Ok(new Tag()));

            // Act
            var result = await _controller.Create(createTagModel);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CreateTag_ReturnsBadRequest_WhenCreationFails()
        {
            // Arrange
            var createTagModel = new CreateTagHttpPostModel();
            _tagServiceMock.Setup(service => service.Create(createTagModel))
                           .ReturnsAsync(ResponseService<Tag>.Error("Error message"));

            // Act
            var result = await _controller.Create(createTagModel);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetAllTags_ReturnsOk_WithTags()
        {
            // Arrange
            var tags = new List<Tag> { new Tag(), new Tag() };
            _tagServiceMock.Setup(service => service.GetAll(It.IsAny<string>())).ReturnsAsync(tags);

            // Act
            var result = await _controller.GetAll("testuser");

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteTag_ReturnsBadRequest_WhenDeletionFails()
        {
            // Arrange
            const int tagId = 1;
            _tagServiceMock.Setup(service => service.DeleteTag(tagId))
                           .ReturnsAsync(ResponseService<bool>.Error("Error message"));

            // Act
            var result = await _controller.Delete(tagId);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
