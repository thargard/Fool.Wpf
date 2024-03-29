﻿using Microsoft.AspNetCore.Mvc;

namespace Fool.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class CardDrawsController : ControllerBase
{
    [HttpPost]
    public bool GetOneCard(int playerId)
    {
        if (playerId != CommonState.SharedState.CurrentMovePlayerId)
            return false;
        Player player1 = CommonState.SharedState.Players.Single(p => p.Id == playerId);
        player1.Hand.Add(CommonState.SharedState.GetOneCard());

        /*CommonState.SharedState.LastTurnTime= DateTime.Now;
        if (player1.Id == 1) CommonState.SharedState.CurrentMovePlayerId = 2;
        else CommonState.SharedState.CurrentMovePlayerId = 1;*/
        return true;
    }
}