using Microsoft.AspNetCore.Mvc;
using BGS.GameAbstractions.Interfaces;
using Catan.Backend.Models;

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
        public IActionResult Execute(Guid gameId, [FromBody] CommandRequestDto request)
        {
            if (!_gameManager.TryGetGame(gameId, out var game))
                return NotFound();

            try
            {
                var result = game.Execute(request);
                return Ok(result);
            }

            catch (BadRequestException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpGet("allGames")]
        public IActionResult GetAllGamesIds()
        {
            var ids = _gameManager.GetAllGamesIds();

            return Ok(ids);
        }
    }
}