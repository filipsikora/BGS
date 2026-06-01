using BGS.GameAbstractions.Interfaces;
using Catan.Backend.Models;
using BGS.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using BGS.Shared.Data;
using BGS.Backend.Interfaces;

namespace BGS.Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly IGameManager _gameManager;
        private readonly IGameFactoryMapper _factoryMapper;
        private readonly IGameRepository _gameRepository;

        public GamesController(IGameManager gameManager, IGameFactoryMapper factoryMapper, IGameRepository gameRepository)
        {
            _gameManager = gameManager;
            _factoryMapper = factoryMapper;
            _gameRepository = gameRepository;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateGame([FromBody] CreateGameRequestDto request)
        {
            if (!Enum.TryParse<EnumGames>(request.GameType, out var gameType))
                return BadRequest($"Failed to parse GameType: {gameType}");

            var factory = _factoryMapper.GetFactory(gameType);

            if (request.PlayerNumber < factory.MinPlayers || request.PlayerNumber > factory.MaxPlayers)
                return BadRequest("Wrong player number");

            var gameId = _gameManager.CreateGame(factory, request.PlayerNumber);
            var game = _gameManager.GetGame(gameId);

            var gameStateString = game.GetGameStateDataString();

            await _gameRepository.SaveGameAsync(gameId, gameStateString, request.GameType, "Created");

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
                return StatusCode(500, new { error = $"{ex}" });
            }
        }

        [HttpGet("allGames")]
        public IActionResult GetAllGamesIds()
        {
            var ids = _gameManager.GetAllGamesIds();

            return Ok(ids);
        }

        [HttpGet("{gameId}/queries/{queryName}")]
        public IActionResult Query(Guid gameId, string queryName)
        {
            if (!_gameManager.TryGetGame(gameId, out var game))
                return NotFound();

            var queryParams = HttpContext.Request.Query;

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