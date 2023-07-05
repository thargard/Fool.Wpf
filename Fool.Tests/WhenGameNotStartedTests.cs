using FluentAssertions;
using Fool.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Fool.Tests;

public sealed class WhenGameNotStartedTests
{
    private readonly MoveController _moveController = new();

    [Fact]
    public void IncorrectPlayerId()
    {
        _moveController.Post(2, "skip").Should().BeOfType<BadRequestObjectResult>()
            .Subject.Value.Should().Be("The current game is not started or finished!");
    }

}