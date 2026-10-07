using ExpChatApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExpChatApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController(IChatService _chatService) : ControllerBase
    {
        [HttpPost("GetConversation")]
        public  async Task<IActionResult> GetConversation([FromBody] List<int> userIds)
        {
            var conversationId =await _chatService.GetOrCreateConversationAsync(userIds);
            return Ok(conversationId);
        }

        [HttpGet("GetUserMsgs")]
        public async Task<IActionResult> GetChatMessages(int convId)
        {
             var messages =await _chatService.GetChatMsg(convId);
            return Ok(messages);
        }
        [HttpGet("GetAllChat")]
        public async Task<IActionResult> GetAllChat()
        {

            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var chat = await _chatService.GetAllChatAsync(userId);
            return Ok(chat);
        }






    }
}
