using BGS.GameAbstractions.Interfaces;
using Catan.Backend.Models;
using BGS.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using BGS.Shared.Data;
using BGS.Backend.Interfaces;
using BGS.Backend.Networking;

namespace BGS.Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly IGameManager _gameManager;
        private readonly IGameFactoryMapper _factoryMapper;
        private readonly IGameRepository _gameRepository;
        private readonly ISocketManager _socketManager;

        public GamesController(IGameManager gameManager, IGameFactoryMapper factoryMapper, IGameRepository gameRepository, ISocketManager socketManager)
        {
            _gameManager = gameManager;
            _factoryMapper = factoryMapper;
            _gameRepository = gameRepository;
            _socketManager = socketManager;
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

            await _gameRepository.SaveGameAsync(gameId, gameStateString, request.GameType.ToString(), game.State.ToString());

            return Ok(new CreateGameResponseDto { GameId = gameId });
        }

        [HttpPost("join")]
        public async Task<IActionResult> JoinGame([FromBody] JoinGameRequestDto request)
        {
            if (request == null)
                return BadRequest("Request body is missing");

            if (!_gameManager.TryGetGame(request.GameId, out var game))
                return NotFound();

            var joinResult = game.JoinGame(request.PlayerToken, request.PlayerName);

            switch (joinResult.JoinStatus)
            {
                case EnumJoinStatus.Success:
                    var gameStateString = game.GetGameStateDataString();
                    await _gameRepository.SaveGameAsync(game.GameId, gameStateString, game.GameType.ToString(), game.State.ToString());

                    return Ok(new JoinGameResponseDto
                    {
                        GameId = game.GameId,
                        PlayerToken = joinResult.PlayerToken!.Value,
                        Payload = joinResult.Payload!
                    });

                case EnumJoinStatus.GameFull:
                    return BadRequest(joinResult.Message);

                case EnumJoinStatus.GameStarted:
                    return BadRequest(joinResult.Message);

                case EnumJoinStatus.TokenNotRecognized:
                    return BadRequest(joinResult.Message);

                default:
                    return StatusCode(500);
            }
        }

        [HttpGet("{gameId}/{playerToken}/socket")]
        public async Task<IActionResult> RegisterSocket(Guid gameId, Guid playerToken)
        {
            if (!HttpContext.WebSockets.IsWebSocketRequest)
                return BadRequest();

            if (!_gameManager.TryGetGame(gameId, out var game))
                return NotFound();

            if (!game.PlayerTokens.ContainsKey(playerToken))
                return Unauthorized();

            var socket = await HttpContext.WebSockets.AcceptWebSocketAsync();

            await _socketManager.HandleConnection(playerToken, socket);

            return new EmptyResult();
        }

        [HttpPost("{gameId}/{playerToken}/command")]
        public async Task<IActionResult> Execute(Guid gameId, Guid playerToken, [FromBody] CommandRequestDto request)
        {
            if (!_gameManager.TryGetGame(gameId, out var game))
                return NotFound();

            if (request == null)
                return BadRequest("Request body is missing");

            var playerId = _gameManager.GetPlayerIdFromToken(game, playerToken);

            try 
            {
                var result = game.Execute(request, playerId);

                await _gameRepository.SaveGameAsync(gameId, game.GetGameStateDataString(), game.GameType.ToString(), game.State.ToString());

                await _socketManager.Broadcast(result.GameUpdates);

                return Ok(result.CommandResponse);
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