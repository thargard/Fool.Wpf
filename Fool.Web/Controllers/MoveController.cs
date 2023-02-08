using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Security.AccessControl;

namespace Fool.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class MoveController : ControllerBase
{
    [HttpPost]
    public bool Post(int playerId, string card)
    {
        if (playerId != CommonState.SharedState.CurrentMovePlayerId)
            return false;

        // Change CommonState.SharedState
        CommonState.SharedState.CardOnTheTable = card;

        Player player1 = CommonState.SharedState.Players.Single(p => p.Id == playerId);
        player1.Hand.Remove(card);
        if (player1.Id == 1) CommonState.SharedState.CurrentMovePlayerId = 2;
        else CommonState.SharedState.CurrentMovePlayerId = 1;
        return true;
    }
}