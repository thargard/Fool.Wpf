using Microsoft.AspNetCore.Mvc;

namespace Fool.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class MoveController : ControllerBase
{
    [HttpPost]
    public void Post(string card)
    {
        // Change CommonState.SharedState

    }
}