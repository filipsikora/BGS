using BGS.Shared.Dtos;
using System.Net.WebSockets;

namespace BGS.Backend.Networking
{
    public interface ISocketManager
    {
        public void Remove(Guid playerToken);
        public Task HandleConnection(Guid playerToken, WebSocket socket);
        public Task Send(GameUpdateDto update);
        public Task Broadcast(IEnumerable<GameUpdateDto> updatesList);
    }
}
