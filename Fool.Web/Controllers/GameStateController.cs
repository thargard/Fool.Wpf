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

            var gs = new PlayerGameState() {
                CardOnTheTable = CommonState.SharedState.CardOnTheTable,
                Hand = CommonState.SharedState.Players.Single(p=>p.Id == playerId).Hand,
                TimeToMove = TimeSpan.FromSeconds(30)
            };

            return gs;
        }
    }
}