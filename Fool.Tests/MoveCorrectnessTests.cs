using CommonClasses;
using FluentAssertions;
using Fool.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace Fool.Tests;

public sealed class MoveCorrectnessTests
{
    private readonly MoveController _moveController = new();

    public MoveCorrectnessTests()
    {
        new GameStateController().StartNewGame(1);
        CommonState.SharedState.CardOnTheTable = new Card(CardValue.Nine, CardSuit.Clubs);
    }

    [Fact]
    public void CanPlayCardWithTheSameValue()
    {
        CommonState.SharedState.Players[1].Hand[0] = new Card(CardValue.Nine, CardSuit.Diamonds);
        _moveController.Post(2, CommonState.SharedState.Players[1].Hand[0].Name).Should().BeOfType<StatusCodeResult>()
            .Subject.StatusCode.Should().Be(StatusCodes.Status201Created);
    }

    [Fact]
    public void CanPlayCardWithTheSameSuit()
    {
        CommonState.SharedState.Players[1].Hand[0] = new Card(CardValue.Jack, CardSuit.Clubs);
        _moveController.Post(2, CommonState.SharedState.Players[1].Hand[0].Name).Should().BeOfType<StatusCodeResult>()
            .Subject.StatusCode.Should().Be(StatusCodes.Status201Created);
    }

    [Fact]
    public void CannotPlayCardWithDifferentSuitAndValue()
    {
        CommonState.SharedState.Players[1].Hand[0] = new Card(CardValue.King, CardSuit.Diamonds);
        _moveController.Post(2, CommonState.SharedState.Players[1].Hand[0].Name).Should().BeOfType<StatusCodeResult>()
            .Subject.StatusCode.Should().Be(StatusCodes.Status406NotAcceptable);
    }

    [Fact]
    public void CanAlwaysPlayQueen()
    {
        CommonState.SharedState.Players[1].Hand[0] = new Card(CardValue.Queen, CardSuit.Spades);
        _moveController.Post(2, CommonState.SharedState.Players[1].Hand[0].Name).Should().BeOfType<StatusCodeResult>()
            .Subject.StatusCode.Should().Be(StatusCodes.Status300MultipleChoices);
    }

}