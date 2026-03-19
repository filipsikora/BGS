using Microsoft.AspNetCore.Mvc;
using BGS.Backend.GameManagement;

namespace BGS.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameManager _gameManager;

        public GameController(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        [HttpPost]
        public IActionResult CreateGame()
        {
            var gameId = _gameManager.CreateGame();

            return Ok(new { gameId });
        }

        [HttpPost("{gameId}/commands")]
        public IActionResult Execute(Guid gameId, [FromBody] object request)
        {
            if (!_gameManager.TryGetGame(gameId, out var game))
                return NotFound();

            var result = game.Execute(request);

            return Ok(result);
        }

        [HttpGet("allGames")]
        public IActionResult GetAllGamesIds()
        {
            var ids = _gameManager.GetAllGamesIds();

            return Ok(ids);
        }
    }
}