using Catan.Core.Snapshots.Persistence;
using Newtonsoft.Json;

namespace Catan.Backend.Helpers
{
    public static class GameStateSerializer
    {
        public static string SerializeGameState(GameStateSnapshot gameStateSnapshot) => JsonConvert.SerializeObject(gameStateSnapshot);

        public static GameStateSnapshot DeserializeGameState(string json) => JsonConvert.DeserializeObject<GameStateSnapshot>(json)!;
    }
}
