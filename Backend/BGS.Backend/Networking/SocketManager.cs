using BGS.Shared.Dtos;
using Newtonsoft.Json;
using System.Net.WebSockets;
using System.Text;

namespace BGS.Backend.Networking
{
    public class SocketManager : ISocketManager
    {
        private Dictionary<Guid, WebSocket> _connections = new();

        public void Register(Guid playerToken, WebSocket socket)
        {
            _connections[playerToken] = socket;
        }

        public void Remove(Guid playerToken)
        {
            _connections.Remove(playerToken);
        }

        public async Task Send(GameUpdateDto gameUpdate)
        {
            if (!_connections.TryGetValue(gameUpdate.PlayerToken, out var socket))
                return;

            if (socket.State != WebSocketState.Open)
                return;

            var json = JsonConvert.SerializeObject(new
            {
                gameUpdate.Type,
                gameUpdate.Payload
            });

            var bytes = Encoding.UTF8.GetBytes(json);

            await socket.SendAsync(bytes, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public async Task Broadcast(IEnumerable<GameUpdateDto> updates)
        {
            foreach (var update in updates)
            {
                await Send(update);
            }
        }
        
        public async Task HandleConnection(Guid playerToken, WebSocket socket)
        {
            Register(playerToken, socket);

            try
            {
                var buffer = new byte[1];

                while (socket.State == WebSocketState.Open)
                {
                    var result = await socket.ReceiveAsync(buffer, CancellationToken.None);

                    if (result.MessageType == WebSocketMessageType.Close)
                        break;
                }
            }

            finally
            {
                Remove(playerToken);

                await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
            }
        }
    }
}