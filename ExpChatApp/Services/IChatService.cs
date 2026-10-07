using ExpChatApp.Entity;
using ExpChatApp.Models;

namespace ExpChatApp.Services
{
    public interface IChatService
    {
        public Task<int> GetOrCreateConversationAsync(List<int> userIds,string type="Individual");
      
       
        public Task<Message> AddMessage(int conversationId, int senderId, string message);

        public Task<List<Message>> GetChatMsg(int convId);

        public Task<List<ChatDto>> GetAllChatAsync(int userId);
        


    }
}
