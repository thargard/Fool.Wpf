using CommonClasses;
using Microsoft.AspNetCore.Mvc;

namespace Fool.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameStateController : ControllerBase
    {
        [HttpGet]
        public PlayerGameState? Get(int playerId)
        {
            // Get from CommonState.SharedState
            if (CommonState.SharedState.Players.Count == 0) { return null; }
            var turnTime = 31 - (DateTime.Now - CommonState.SharedState.LastTurnTime).TotalSeconds;
            var gs = new PlayerGameState()
            {
                CardOnTheTable = CommonState.SharedState.CardOnTheTable,
                Hand = CommonState.SharedState.Players.Single(p => p.Id == playerId).Hand,
                TimeToMove = TimeSpan.FromSeconds(turnTime),
                IsMineTurn = CommonState.SharedState.CurrentMovePlayerId == playerId,
                TopCardSuit = CommonState.SharedState.TopCardSuit
            };

            return gs;
        }

        [HttpPost]
        public void StartNewGame(int playerId)
        {
            CommonState.SharedState.Deck.FillDeck();

            CommonState.SharedState.LastTurnTime = DateTime.Now;
            CommonState.SharedState.Players.Clear();

            Player pl1 = new Player() { Id = 1, Name = "Boris", Hand = new List<Card>() };
            Player pl2 = new Player() { Id = 2, Name = "Gleb", Hand = new List<Card>() };

            CommonState.SharedState.Players.Add(pl1);
            CommonState.SharedState.Players.Add(pl2);

            foreach (var item in CommonState.SharedState.Players)
                for (int i = 0; i < 4; i++)
                    item.Hand.Add(CommonState.SharedState.GetOneCard());


            CommonState.SharedState.CurrentMovePlayerId = playerId;
            CommonState.SharedState.CardOnTheTable = CommonState.SharedState.GetOneCard();
            CommonState.SharedState.TopCardSuit = CommonState.SharedState.CardOnTheTable.Suit;
            CommonState.SharedState.GameIsGoing = true;
        }

        [HttpPost]
        [Route("CurrentSuit")]
        public void ChangeSuit(string suit)
        {
            CommonState.SharedState.TopCardSuit = suit;
        }

        
        [HttpGet]
        [Route("PlayerToMove")]
        public int? Get()
        {
            if (CommonState.SharedState.Players.Count == 0) { return null; }
            return CommonState.SharedState.CurrentMovePlayerId;
        }
    }
}