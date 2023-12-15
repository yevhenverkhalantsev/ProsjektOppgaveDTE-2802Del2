using Microsoft.AspNetCore.Mvc;
using Moq;
using ProsjektOppgaveWebAPI.Controllers;
using ProsjektOppgaveWebAPI.Services.Response;
using ProsjektOppgaveWebAPI.Services.TagServices;
using ProsjektOppgaveWebAPI.Services.TagServices.Models;

namespace ProsjektOppgaveWebAPITest;

public class TagControllerTest
{
    private readonly Mock<ITagService> _serviceMock;
    private readonly TagController _controller;

    public TagControllerTest()
    {
        _serviceMock = new Mock<ITagService>();
        _controller = new TagController(_serviceMock.Object);
    }

    [Fact]
    public async Task Create_ReturnsBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        _controller.ModelState.AddModelError("error", "some error");
        var tagModel = new CreateTagHttpPostModel();

        // Act
        var result = await _controller.Create(tagModel);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Create_ReturnsOk_WithTagId_WhenCreationIsSuccessful()
    {
        // Arrange
        var tagModel = new CreateTagHttpPostModel();
        _serviceMock.Setup(service => service.Create(tagModel))
            .ReturnsAsync(ResponseService<long>.Ok(1));

        // Act
        var result = await _controller.Create(tagModel);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(1L, okResult.Value);
        _serviceMock.Verify(x => x.Create(tagModel), Times.Once);
    }

    [Fact]
    public async Task Create_ReturnsBadRequest_WithErrorMessage_WhenCreationFails()
    {
        // Arrange
        var tagModel = new CreateTagHttpPostModel();
        _serviceMock.Setup(service => service.Create(tagModel))
            .ReturnsAsync(ResponseService<long>.Error("Error creating tag"));

        // Act
        var result = await _controller.Create(tagModel);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result); 
    }
}
