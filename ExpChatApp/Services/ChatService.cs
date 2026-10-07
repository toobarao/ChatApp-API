
using ExpChatApp.Data;
using ExpChatApp.Entity;
using ExpChatApp.Migrations;
using ExpChatApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static AutoMapper.Internal.ExpressionFactory;

namespace ExpChatApp.Services
{
    public class ChatService(ChatAppDbContext dbContext) : IChatService
    {
      

        public async Task<Message> AddMessage(int conversationId, int senderId, string content)
        {
           var message=new Message
            {
                ConversationId = conversationId,
                SenderId = senderId,
                Content = content,
                SentAt = DateTime.Now
            };
            dbContext.messages.Add(message);
            await dbContext.SaveChangesAsync();

            return message;
        }

        public async Task<List<ChatDto>> GetAllChatAsync(int userId)
        {
          
            var chats = await dbContext.conversation_members
                        .Where(cm => cm.UserId == userId)
                        .Select(cm => new ChatDto
                        {
                            ConvId = cm.ConversationId,

                            Name = cm.Conversation.ConversationType == "Individual"
                                ? cm.Conversation.ConversationMembers
                                    .Where(m => m.UserId != userId)
                                    .Select(m => m.User.Name)
                                    .FirstOrDefault()
                                : null,

                            members = cm.Conversation.ConversationMembers
                                .Where(m => m.UserId != userId)
                                .Select(m => m.UserId)
                                .ToList(),

                            LastMsg = cm.Conversation.Messages
                                .OrderByDescending(m => m.SentAt)
                                .Select(m => m.Content)
                                .FirstOrDefault(),

                            Date = cm.Conversation.Messages
                                .OrderByDescending(m => m.SentAt)
                                .Select(m => (DateTime?)m.SentAt)
                                .FirstOrDefault()
                        })
                        .ToListAsync();
            return chats;


        }

        public async  Task<List<Message>> GetChatMsg(int convId)
        {
            var messages=await dbContext.messages.Where(x=>x.ConversationId==convId).ToListAsync();
            return messages;


        }

        public async Task<int> GetOrCreateConversationAsync(List<int> userIds, string type="Individual")
        {
           
            var conversation =await dbContext.conversations
                .Include(c => c.ConversationMembers)
                .Where(c => c.ConversationMembers.Count == userIds.Count && 
                            c.ConversationMembers.All(cm => userIds.Contains(cm.UserId))&& c.ConversationType==type)
                .FirstOrDefaultAsync();
            if (conversation != null)
                return conversation.ConversationId;
            else {
                Conversation newConversation = new Conversation();
              

                foreach (var item in userIds)
                {
                    newConversation.ConversationMembers.Add(new ConversationMember { UserId = item });

                }
                dbContext.conversations.Add(newConversation);
                await dbContext.SaveChangesAsync();
                return newConversation.ConversationId;
            }
        }
        

       
    }
}
