using Microsoft.AspNetCore.Mvc;

namespace Fool.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameStateController : ControllerBase
    {
        
        [HttpGet]
        public PlayerGameState Get()
        {
            // Get from CommonState.SharedState

            var gs =

                new PlayerGameState()
                {
                    CardOnTheTable = "дама пик",
                    Hand = new string[] { "afsd", "asdf" },
                    TimeToMove = TimeSpan.FromSeconds(30)
                };
            
            return gs;
        }
    }
}