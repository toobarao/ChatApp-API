using ExpChatApp.Services;
using Microsoft.AspNetCore.SignalR;

namespace ExpChatApp.Hubs
{

    public class ChatHub(IChatService chatService):Hub
    {
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine("===== SIGNALR CONNECTED =====");

            await base.OnConnectedAsync();
        }
        public async Task SendMessage(int convId,int userId, string message)
        {
           var msg = await chatService.AddMessage(convId, userId, message);

            Console.WriteLine($"{userId}: {message}");
            await Clients.Group(convId.ToString()).SendAsync("ReceiveMessage",msg);

            //await Clients.User(userId.ToString()).SendAsync(
            //    "ReceiveMessage",
            //    Context.UserIdentifier,
            //    msg
            //);
        }
        public async Task JoinConversation(int convId)
        {
            await Groups.AddToGroupAsync(
                Context.ConnectionId,
                convId.ToString()
            );

            Console.WriteLine(
                $"Connection {Context.ConnectionId} joined group {convId}"
            );
        }

    }
}
