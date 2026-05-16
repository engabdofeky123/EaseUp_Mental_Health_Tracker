using Application.DTO.Acheivements;
using Application.Features.Exercises.Commands.Complete;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Models;
using FluentAssertions;
using Moq;
using Xunit;

public class CompleteExerciseHandlerTests
{
    private readonly Mock<IExerciseRepository> _repoMock;
    private readonly Mock<IGetStudentByUserIdService> _studentServiceMock;
    private readonly Mock<IAcheivementsService> _achievementServiceMock;

    private readonly CompleteExerciseHandler _handler;

    public CompleteExerciseHandlerTests()
    {
        _repoMock = new Mock<IExerciseRepository>();
        _studentServiceMock = new Mock<IGetStudentByUserIdService>();
        _achievementServiceMock = new Mock<IAcheivementsService>();

        _handler = new CompleteExerciseHandler(
            _repoMock.Object,
            _studentServiceMock.Object,
            _achievementServiceMock.Object
        );
    }

    [Fact]
    public async Task Handle_Should_Increment_Score_And_Save()
    {
        // Arrange
        var command = new CompleteExerciseCommand(1);

        var student = new Student
        {
            Id = 10,
            Total_Exercise_Score = 10
        };

        _studentServiceMock
            .Setup(x => x.GetStudentAsyncByUserID(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(student);

        _repoMock
            .Setup(x => x.SaveDB())
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeTrue();

        student.Total_Exercise_Score.Should().Be(15);

        _repoMock.Verify(x => x.SaveDB(), Times.Once);
        _achievementServiceMock.Verify(x => x.AssignAcheivement(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
    }

    [Fact]
    public async Task Handle_Should_Assign_Achievement_When_Score_Reaches_Threshold()
    {
        // Arrange
        var command = new CompleteExerciseCommand(1);

        var student = new Student
        {
            Id = 10,
            Total_Exercise_Score = 22
        };

        _studentServiceMock
            .Setup(x => x.GetStudentAsyncByUserID(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(student);

        _repoMock
            .Setup(x => x.SaveDB())
            .Returns(Task.CompletedTask);

        _achievementServiceMock
    .Setup(x => x.AssignAcheivement(10, 3))
    .ReturnsAsync(new AssignAcheivementToStudentResult());

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeTrue();

        student.Total_Exercise_Score.Should().Be(27);

        _achievementServiceMock.Verify(
            x => x.AssignAcheivement(10, 3),
            Times.Once
        );
    }

    [Fact]
    public async Task Handle_Should_Return_False_When_Student_Not_Found()
    {
        // Arrange
        _studentServiceMock
            .Setup(x => x.GetStudentAsyncByUserID(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Student)null);

        var command = new CompleteExerciseCommand(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeFalse();

        _repoMock.Verify(x => x.SaveDB(), Times.Never);
        _achievementServiceMock.Verify(x => x.AssignAcheivement(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
    }

}