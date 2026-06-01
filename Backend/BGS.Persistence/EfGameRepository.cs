using BGS.GameAbstractions.Interfaces;
using BGS.Persistence.Context;
using BGS.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace BGS.Persistence
{
    public class EfGameRepository : IGameRepository
    {
        private readonly BgsDBContext _db;
        
        public EfGameRepository(BgsDBContext db)
        {
            _db = db;
        }

        public async Task SaveGameAsync(Guid gameId, string stateJson, string gameType, string status)
        {
            var existing = await _db.Games.FirstOrDefaultAsync(g => g.GameId == gameId);

            if (existing == null)
            {
                var entity = new GameEntity
                {
                    GameId = gameId,
                    GameType = gameType,
                    StateJson = stateJson,
                    Status = status,
                    UpdatedAt = DateTime.UtcNow
                };

                _db.Games.Add(entity);
            }

            else
            {
                existing.StateJson = stateJson;
                existing.StateJson = status;
                existing.UpdatedAt = DateTime.UtcNow;
            }

            await _db.SaveChangesAsync();
        }

        public async Task<string?> LoadStateAsync(Guid gameId)
        {
            var game = await _db.Games.FirstOrDefaultAsync(g => g.GameId == gameId);

            return game?.StateJson;
        }
    }
}