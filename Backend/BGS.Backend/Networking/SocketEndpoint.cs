using BGS.Backend.Networking;

namespace BGS.Networking.Websockets
{
    public class SocketEndpoint
    {
        private readonly SocketManager _socketManager;

        public SocketEndpoint(SocketManager socketManager)
        {
            _socketManager = socketManager;
        }

        public async Task Handle(HttpContext context)
        {
            if (!context.WebSockets.IsWebSocketRequest)
            {
                context.Response.StatusCode = 400;

                return;
            }

            var socket = await context.WebSockets.AcceptWebSocketAsync();
        }
    }
}