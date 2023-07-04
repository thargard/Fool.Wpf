using CommonClasses;
using FluentAssertions;
using FluentAssertions.Execution;
using Fool.Web;
using Fool.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fool.Tests
{
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
    public sealed class MoveCorrectnessTests
    {
        private readonly MoveController _moveController = new();

        public MoveCorrectnessTests()
        {
            new GameStateController().StartNewGame(1);
        }

        [Fact]
        public void CorrectPlayerToMove()
        {
            _moveController.Post(2, "skip").Should().BeOfType<StatusCodeResult>()
                .Subject.StatusCode.Should().Be(StatusCodes.Status406NotAcceptable);
        }
     
        [Fact]
        public void CanAlwaysPlayQueen()
        {
            CommonState.SharedState.Players[0].Hand[0] = new Card(CardValue.Queen, CardSuit.Clubs);
            _moveController.Post(1, "skip").Should().BeOfType<StatusCodeResult>()
                .Subject.StatusCode.Should().Be(StatusCodes.Status409Conflict);
        }
    }
}