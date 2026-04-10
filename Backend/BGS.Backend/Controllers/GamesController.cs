using BGS.Backend.Helpers;
using BGS.GameAbstractions.Interfaces;
using Catan.Backend.Models;
using BGS.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using BGS.Shared.Data;

namespace BGS.Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly IGameManager _gameManager;
        private readonly GameFactoryMapper _factoryMapper;

        public GamesController(IGameManager gameManager, GameFactoryMapper factoryMapper)
        {
            _gameManager = gameManager;
            _factoryMapper = factoryMapper;
        }

        [HttpPost("create")]
        public IActionResult CreateGame([FromBody] CreateGameRequestDto request)
        {
            if (!Enum.TryParse<EnumGames>(request.GameType, out var gameType))
                return StatusCode(500, new { error = $"Failed to parse GameType: {gameType}" });


            var factory = _factoryMapper.GetFactory(gameType);
            var gameId = _gameManager.CreateGame(factory);

            return Ok(new CreateGameResponseDto { GameId = gameId });
        }

        [HttpPost("{gameId}/command")]
        public IActionResult Execute(Guid gameId, [FromBody] CommandRequestDto request)
        {
            if (!_gameManager.TryGetGame(gameId, out var game))
                return NotFound();

            if (request == null)
                return BadRequest("Request body is missing");

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

        [HttpGet("{gameId}/queries/{queryName}")]
        public IActionResult Query(Guid gameId, string queryName, [FromQuery] Dictionary<string, StringValues> queryParams)
        {
            if (!_gameManager.TryGetGame(gameId, out var game))
                return NotFound();

            try
            {
                return Ok(game.Query(queryName, queryParams));
            }

            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}