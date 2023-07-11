using CommonClasses;
using FluentAssertions;
using FluentAssertions.Execution;
using Fool.Web;
using Fool.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fool.Tests
{
    public sealed class HasRightToSkipTests
    {
        private readonly MoveController _moveController = new();

        public HasRightToSkipTests()
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
        public void HasRightToSkipMove()
        {
            CommonState.SharedState.Players[0].Hand[0] = new Card(CardValue.Queen, CardSuit.Clubs);
            _moveController.Post(1, "skip").Should().BeOfType<StatusCodeResult>()
                .Subject.StatusCode.Should().Be(StatusCodes.Status409Conflict);
        }

        [Fact]
        public void HasNoRightToSkipWithoutTakingCard()
        {

        }

        [Fact]
        public void HasNoRightToSkipIfPlayed8()
        {
            CommonState.SharedState.Players[0].Hand[0] = new Card(CardValue.Eight, CardSuit.Diamonds);
            _moveController.Post(1, CommonState.SharedState.Players[0].Hand[0].Name);
            _moveController.Post(1, "skip").Should().BeOfType<StatusCodeResult>()
               .Subject.StatusCode.Should().Be(StatusCodes.Status409Conflict);
        }
    }
}