using Microsoft.AspNetCore.SignalR;

namespace BlazorApp1
{
    public class BlazorChatSampleHub: Hub
    {
        public const string HubUrl = "/chat";
        private readonly UserService _userService;

        public BlazorChatSampleHub(UserService userService)
        {
            _userService = userService;
        }

        public async Task Broadcast(string username, string message)
        {
            await Clients.All.SendAsync("Broadcast", username, message);
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"{Context.ConnectionId} connected");
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception e)
        {
            Console.WriteLine($"Disconnected {e?.Message} {Context.ConnectionId}");
            await base.OnDisconnectedAsync(e);
        }
        public async Task Private(string from, string to, string message)
        {
            var connectionId = _userService.GetConnectioIdByName(to);
            if (connectionId != null)
            {
                await Clients.Client(connectionId).SendAsync("Private", from, message);
            }
            else
            {
                await Clients.Caller.SendAsync("Error", $"User '{to}' is not available.");
            }
        }


    }
}
