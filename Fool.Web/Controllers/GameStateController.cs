using Microsoft.AspNetCore.Mvc;

namespace Fool.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameStateController : ControllerBase
    {

        [HttpGet]
        public PlayerGameState Get(int playerId)
        {
            // Get from CommonState.SharedState

            var gs = new PlayerGameState()
            {
                CardOnTheTable = CommonState.SharedState.CardOnTheTable,
                Hand = CommonState.SharedState.Players.Single(p => p.Id == playerId).Hand,
                TimeToMove = TimeSpan.FromSeconds(30),
                IsMineTurn = CommonState.SharedState.CurrentMovePlayerId == playerId,
            };

            return gs;
        }

        [HttpPost]
        public void StartNewGame(int id)
        {
            CommonState.SharedState.Players.Clear();

            Player pl1 = new Player() { Id = 1, Name = "Boris", Hand = new List<string>() };
            Player pl2 = new Player() { Id = 2, Name = "Gleb", Hand = new List<string>() };

            CommonState.SharedState.Players.Add(pl1);
            CommonState.SharedState.Players.Add(pl2);

            foreach (var item in CommonState.SharedState.Players)
                for (int i = 0; i < 4; i++)
                    item.Hand.Add(CommonState.SharedState.GetOneCard());


            CommonState.SharedState.CurrentMovePlayerId = id;
            CommonState.SharedState.CardOnTheTable = CommonState.SharedState.GetOneCard();
        }
    }
}