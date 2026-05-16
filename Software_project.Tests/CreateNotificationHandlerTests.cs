using Application.DTO.Notifications;
using Application.Features.Notification.Commands;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using FluentAssertions;
using Moq;
using Xunit;

public class CreateNotificationHandlerTests
{
    private readonly Mock<INotificationRepository> _repoMock;
    private readonly Mock<INotificationService> _serviceMock;
    private readonly CreateNotificationHandler _handler;

    public CreateNotificationHandlerTests()
    {
        _repoMock = new Mock<INotificationRepository>();
        _serviceMock = new Mock<INotificationService>();

        _handler = new CreateNotificationHandler(
            _repoMock.Object,
            _serviceMock.Object
        );
    }

    [Fact]
    public async Task Handle_Should_Save_And_Send_Notification_When_Data_Is_Valid()
    {
        // Arrange => Make input ready for testing 
        var command = new CreateNotificationCommand(
            UserId: 1,
            Title: "Test Title",
            Message: "Test Message"
        );

        NotificationDto? capturedNotification = null;

        _repoMock /// Fake repo (no communication with DB)
            .Setup(x => x.AddAsync(It.IsAny<NotificationDto>()))
            .Callback<NotificationDto>(n => capturedNotification = n)
            .Returns(Task.CompletedTask);

        _serviceMock  // Fake service (no communication with DB)
            .Setup(x => x.SendToUser(It.IsAny<NotificationDto>()))
            .Returns(Task.CompletedTask);

        // Act
        await _handler.Handle(command, CancellationToken.None); // Test the real code

        // Assert - repository called once
        _repoMock.Verify(x => x.AddAsync(It.IsAny<NotificationDto>()), Times.Once); // => make sure it has been called once

        // Assert - service called once
        _serviceMock.Verify(x => x.SendToUser(It.IsAny<NotificationDto>()), Times.Once); // => make sure it has been called once

        // Assert - data correctness (important upgrade => Revise data 
        capturedNotification.Should().NotBeNull();
        capturedNotification!.UserId.Should().Be(1);
        capturedNotification.Title.Should().Be("Test Title");
        capturedNotification.Message.Should().Be("Test Message");
    }

    [Fact]
    public async Task Handle_Should_Throw_Exception_When_Data_Is_Invalid()
    {
        // Arrange
        var command = new CreateNotificationCommand(
            UserId: 1,
            Title: "",
            Message: ""
        );

        // Act
        Func<Task> act = async () =>
            await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should()
            .ThrowAsync<Exception>()
            .WithMessage("Invalid notification data");
    }

    [Fact]
    public async Task Handle_Should_Not_Fail_When_Service_Fails()
    {
        // Arrange
        var command = new CreateNotificationCommand(
            UserId: 1,
            Title: "Title",
            Message: "Message"
        );

        _repoMock
            .Setup(x => x.AddAsync(It.IsAny<NotificationDto>()))
            .Returns(Task.CompletedTask);

        _serviceMock
            .Setup(x => x.SendToUser(It.IsAny<NotificationDto>()))
            .ThrowsAsync(new Exception("SignalR failed"));

        // Act
        Func<Task> act = async () =>
            await _handler.Handle(command, CancellationToken.None);

        // Assert - should not crash whole flow
        await act.Should().NotThrowAsync();
    }
}