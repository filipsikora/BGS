using Microsoft.AspNetCore.Mvc;
using BGS.GameAbstractions.Interfaces;
using Catan.Shared.Commands;

namespace BGS.Backend.Controllers
{
    [ApiController]
    [Route("game")]

    public class GameController : ControllerBase
    {
        private readonly IGameInstance _game;

        public GameController(IGameInstance game)
        {
            _game = game;
        }

        [HttpGet("test")]
        public object Start()
        {
            return _game.Execute(new StartGameCommand());
        }
    }
}