using CommonClasses;
using Microsoft.AspNetCore.Mvc;

namespace Fool.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class MoveController : ControllerBase
{
    [HttpPost]
    public IActionResult Post(int playerId, string command)
    {
        if (command == "skip")
            return Skip(playerId);
        if (playerId != CommonState.SharedState.CurrentMovePlayerId)
            return StatusCode(StatusCodes.Status406NotAcceptable);

        // Change CommonState.SharedState
        var newCardValue = default(CardValue);
        string[] arr = command.Split(' ');
        switch (arr[0])
        {
            case "6": newCardValue = CardValue.Six; break;
            case "7": newCardValue = CardValue.Seven; break;
            case "8": newCardValue = CardValue.Eight; break;
            case "9": newCardValue = CardValue.Nine; break;
            case "10": newCardValue = CardValue.Ten; break;
            case "Валет": newCardValue = CardValue.Jack; break;
            case "Дама": newCardValue = CardValue.Queen; break;
            case "Король": newCardValue = CardValue.King; break;
            case "Туз": newCardValue = CardValue.Ace; break;
            default: break;
        }

        var suit = Enum.Parse<CardSuit>(arr[1]);
        Card ncard = new Card(newCardValue, suit);

        if (ncard.Value == CardValue.Queen)
        {
            Console.WriteLine("Это дама!"); // просто так
        }
        else
        {
            if (ncard.Suit != CommonState.SharedState.TopCardSuit && ncard.Value != CommonState.SharedState.CardOnTheTable.Value)
                return StatusCode(StatusCodes.Status406NotAcceptable);
        }

        CommonState.SharedState.CardOnTheTable = ncard;

        Player player1 = CommonState.SharedState.Players.Single(p => p.Id == playerId);
        Player player2 = CommonState.SharedState.Players.Single(p => p.Id != playerId);

        var nlist = new List<Card>();
        foreach (var crd in player1.Hand)
            if (crd.Name != ncard.Name)
                nlist.Add(crd);
        player1.Hand.Clear();
        player1.Hand.AddRange(nlist);

        CommonState.SharedState.LastTurnTime = DateTime.Now;
        CommonState.SharedState.TopCardSuit = ncard.Suit; // Присваивать масть верхней карты при завершении хода 

        if (player1.Hand.Count() == 0)
        {
            CommonState.SharedState.GameIsGoing = false;
            return StatusCode(StatusCodes.Status410Gone); // Игра завершена
        }

        if (ncard.Value == CardValue.Eight)
            return StatusCode(StatusCodes.Status401Unauthorized);
        if (ncard.Value == CardValue.Seven)
        {
            player2.Hand.Add(CommonState.SharedState.GetOneCard());
            return StatusCode(StatusCodes.Status403Forbidden);
        }
        if (ncard.Value == CardValue.Six)
        {
            player2.Hand.Add(CommonState.SharedState.GetOneCard());
            player2.Hand.Add(CommonState.SharedState.GetOneCard());
            return StatusCode(StatusCodes.Status403Forbidden);
        }
        if (ncard.Value == CardValue.Ace)
            return StatusCode(StatusCodes.Status403Forbidden);

        if (player1.Id == 1) CommonState.SharedState.CurrentMovePlayerId = 2;
        else CommonState.SharedState.CurrentMovePlayerId = 1;

        if (ncard.Value == CardValue.Queen)
            return StatusCode(StatusCodes.Status300MultipleChoices);

        return StatusCode(StatusCodes.Status201Created);
    }


    private IActionResult Skip(int playerId)
    {
        if (!CommonState.SharedState.GameIsGoing)
            return BadRequest("The current game is not started or finished!");

        if (playerId != CommonState.SharedState.CurrentMovePlayerId)
            return StatusCode(StatusCodes.Status406NotAcceptable);

        var player = CommonState.SharedState.Players.Single(p => p.Id == playerId);

        int coincidence = 0;

        var suit = CommonState.SharedState.CardOnTheTable.Suit;
        var value = CommonState.SharedState.CardOnTheTable.Value;

        /*  if (player.Hand.Any(c => c.Value == CardValue.Queen))
          {
              return StatusCode(StatusCodes.Status409Conflict);
          }
          else
          {
              foreach (var crd in player.Hand)
              {
                  if (crd.Value == value || crd.Suit == suit)
                      coincidence++;
              }

              if (coincidence == 0)
              {
                  if (player.Id == 1) CommonState.SharedState.CurrentMovePlayerId = 2;
                  else CommonState.SharedState.CurrentMovePlayerId = 1;
              }
              if (coincidence >= 1) { return StatusCode(StatusCodes.Status409Conflict); }
          }*/

        if (player.Id == 1) CommonState.SharedState.CurrentMovePlayerId = 2;
        else CommonState.SharedState.CurrentMovePlayerId = 1;

        return StatusCode(StatusCodes.Status201Created);
    }
}