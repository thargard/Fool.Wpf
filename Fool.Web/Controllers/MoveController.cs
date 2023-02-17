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
            case "�����": ncard.Value = 11; break;
            case "����": ncard.Value = 12; break;
            case "������": ncard.Value = 13; break;
            case "���": ncard.Value = 14; break;
            default: break;
        }
        ncard.Suit = arr[1];

        if (ncard.Suit != CommonState.SharedState.CardOnTheTable.Suit && ncard.Value != CommonState.SharedState.CardOnTheTable.Value) 
        {
            return StatusCode(StatusCodes.Status406NotAcceptable);
        }

        CommonState.SharedState.CardOnTheTable = ncard;

        Player player1 = CommonState.SharedState.Players.Single(p => p.Id == playerId);

        var nlist = new List<Card>();
        foreach (var crd in player1.Hand)
            if (crd.Name != ncard.Name)
                nlist.Add(crd);
        player1.Hand.Clear();
        player1.Hand.AddRange(nlist);

        //player1.Hand.Remove(ncard);
        CommonState.SharedState.LastTurnTime = DateTime.Now;
        if (player1.Id == 1) CommonState.SharedState.CurrentMovePlayerId = 2;
        else CommonState.SharedState.CurrentMovePlayerId = 1;
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpGet]
    public int? Get()
    {
        if (CommonState.SharedState.Players.Count == 0) { return null; }
        return CommonState.SharedState.CurrentMovePlayerId;
    }
}