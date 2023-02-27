using CommonClasses;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Security.AccessControl;

namespace Fool.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class MoveController : ControllerBase
{
    [HttpPost]
    [Route("Route")]
    public IActionResult Post(int playerId, string card)
    {
        if (playerId != CommonState.SharedState.CurrentMovePlayerId)
            return StatusCode(StatusCodes.Status406NotAcceptable);

        // Change CommonState.SharedState
        Card ncard = new Card();
        string[] arr = card.Split(' ');
        switch (arr[0])
        {
            case "6": ncard.Value = 6; break;
            case "7": ncard.Value = 7; break;
            case "8": ncard.Value = 8; break;
            case "9": ncard.Value = 9; break;
            case "10": ncard.Value = 10; break;
            case "Валет": ncard.Value = 11; break;
            case "Дама": ncard.Value = 12; break;
            case "Король": ncard.Value = 13; break;
            case "Туз": ncard.Value = 14; break;
            default: break;
        }
        ncard.Suit = arr[1];

        if (ncard.Suit != CommonState.SharedState.CardOnTheTable.Suit && ncard.Value != CommonState.SharedState.CardOnTheTable.Value)
            return StatusCode(StatusCodes.Status406NotAcceptable);

        CommonState.SharedState.CardOnTheTable = ncard;

        Player player1 = CommonState.SharedState.Players.Single(p => p.Id == playerId);
        Player player2 = CommonState.SharedState.Players.Single(p => p.Id != playerId);

        var nlist = new List<Card>();
        foreach (var crd in player1.Hand)
            if (crd.Name != ncard.Name)
                nlist.Add(crd);
        player1.Hand.Clear();
        player1.Hand.AddRange(nlist);

        //player1.Hand.Remove(ncard);
        CommonState.SharedState.LastTurnTime = DateTime.Now;
        if (ncard.Value == 8)
            return StatusCode(StatusCodes.Status403Forbidden);
        if (ncard.Value == 7)
        {
            player2.Hand.Add(CommonState.SharedState.GetOneCard());
            return StatusCode(StatusCodes.Status403Forbidden);
        }
        if (ncard.Value == 6)
        {
            player2.Hand.Add(CommonState.SharedState.GetOneCard());
            player2.Hand.Add(CommonState.SharedState.GetOneCard());
            return StatusCode(StatusCodes.Status403Forbidden);
        }

        if (player1.Id == 1) CommonState.SharedState.CurrentMovePlayerId = 2;
        else CommonState.SharedState.CurrentMovePlayerId = 1;
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPost]
    [Route("RouteN")]
    public IActionResult Post(int playerId)
    {
        if (playerId != CommonState.SharedState.CurrentMovePlayerId)
            return StatusCode(StatusCodes.Status406NotAcceptable);

        Player player1 = CommonState.SharedState.Players.Single(p => p.Id == playerId);

        int coincidence = 0;

        string suit = CommonState.SharedState.CardOnTheTable.Suit;
        int value = CommonState.SharedState.CardOnTheTable.Value;

        var card = new Card() { Value = 12 };

        if (player1.Hand.Contains(card)) { return StatusCode(StatusCodes.Status409Conflict); }
        else
        {
            foreach (var crd in player1.Hand)
            {
                if (crd.Value == value || crd.Suit == suit)
                    coincidence++;
            }

            if (coincidence == 0)
            {
                if (player1.Id == 1) CommonState.SharedState.CurrentMovePlayerId = 2;
                else CommonState.SharedState.CurrentMovePlayerId = 1;
            }
            if (coincidence >= 1) { return StatusCode(StatusCodes.Status409Conflict); }
        }
        return StatusCode(StatusCodes.Status201Created);
    }



    [HttpGet]
    public int? Get()
    {
        if (CommonState.SharedState.Players.Count == 0) { return null; }
        return CommonState.SharedState.CurrentMovePlayerId;
    }
}