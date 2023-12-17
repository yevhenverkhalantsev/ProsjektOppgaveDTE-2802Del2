using Microsoft.AspNetCore.Mvc;
using Moq;
using ProsjektOppgaveWebAPI.Controllers;
using ProsjektOppgaveWebAPI.Models.Tag;
using ProsjektOppgaveWebAPI.Services.TagServices;
using ProsjektOppgaveWebAPI.Services.Response;
using ProsjektOppgaveWebAPI.Database.Entities;
using ProsjektOppgaveWebAPI.Services.TagServices.Models;

namespace ProsjektOppgaveWebAPITest;

public class TagControllerTests
{
    private readonly Mock<ITagService> _mockTagService;
    private readonly TagController _tagController;

    public TagControllerTests()
    {
        _mockTagService = new Mock<ITagService>();
        _tagController = new TagController(_mockTagService.Object, null);
    }

    [Fact]
    public async Task Create_WithInvalidModelState_ReturnsBadRequest()
    {
        // Arrange
        var invalidModel = new CreateTagHttpPostModel();
        _tagController.ModelState.AddModelError("TestError", "Model state is invalid");

        // Act
        var result = await _tagController.Create(invalidModel);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Create_WhenSuccessful_ReturnsOkWithCreatedTag()
    {
        // Arrange
        var validModel = new CreateTagHttpPostModel { Name = "TestTag" };
        var createdTag = new Tag { Id = 1, Name = "TestTag" };
        _mockTagService.Setup(s => s.Create(validModel))
            .ReturnsAsync(ResponseService<Tag>.Ok(createdTag));

        // Act
        var result = await _tagController.Create(validModel);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var resultValue = Assert.IsType<HttpGetTagModel>(okResult.Value);
        Assert.Equal(createdTag.Id, resultValue.Id);
        Assert.Equal(createdTag.Name, resultValue.Name);
    }

    [Fact]
    public async Task Create_WhenFails_ReturnsBadRequestWithErrorMessage()
    {
        // Arrange
        var validModel = new CreateTagHttpPostModel { Name = "TestTag" };
        _mockTagService.Setup(s => s.Create(validModel))
            .ReturnsAsync(ResponseService<Tag>.Error("Error creating tag"));

        // Act
        var result = await _tagController.Create(validModel);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Error creating tag", badRequestResult.Value);
    }
}
