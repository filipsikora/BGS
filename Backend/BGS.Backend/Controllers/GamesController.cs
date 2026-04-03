using Microsoft.AspNetCore.Mvc;
using BGS.GameAbstractions.Interfaces;
using Catan.Backend.Models;
using Catan.Shared.Dtos;
using BGS.Backend.Helpers;

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
            var factory = _factoryMapper.GetFactory(request.GameType);
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

        [HttpGet("{gameId}/queries/board")]
        public IActionResult QueryBoard(Guid gameId)
        {
            if (!_gameManager.TryGetGame(gameId, out var game))
                return NotFound();

            try
            {
                var boardData = game.Query("board");

                return Ok(boardData);
            }
            
            catch
            {
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpGet("{gameId}/queries/player-data/{playerId}")]
        public IActionResult QueryPlayerData(Guid gameId, int playerId)
        {
            if (!_gameManager.TryGetGame(gameId, out var game))
                return NotFound();

            try
            {
                var playerData = game.Query("player-Data", playerId);

                return Ok(playerData);
            }

            catch
            {
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpGet("{gameId}/queries/player-cards/{playerId")]
        public IActionResult QueryPlayerCards(Guid gameId, int playerId)
        {
            if (!_gameManager.TryGetGame(gameId, out var game))
                return NotFound();

            try
            {
                var playerCards = game.Query("player-cards", playerId);

                return Ok(playerCards);
            }

            catch
            {
                return StatusCode(500, new { error = "Internal server error" });
            }
        }
    }
}