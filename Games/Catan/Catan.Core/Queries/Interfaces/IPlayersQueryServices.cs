using Catan.Core.Snapshots.ClientQueries;

namespace Catan.Core.Queries.Interfaces
{
    public interface IPlayersQueryService
    {
        PlayerResourcesSnapshot GetPlayersCards(int playerId);

        PlayerDataSnapshot GetPlayersData(int playerId);

        CurrentPlayerIdSnapshot GetCurrentPlayerId();

        List<PlayerNameSnapshot> GetAllPlayersNames();

        List<PlayerNameSnapshot> GetSomePlayersNames(List<int> potentialVictimsIds);

        List<PlayerNameSnapshot> GetNotCurrentPlayersNames();

        PlayerResourcesSnapshot GetVictimsCards();

        FullPlayerSnapshot GetFullPlayerData(int playerId);
    }
}