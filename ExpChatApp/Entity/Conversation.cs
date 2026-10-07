namespace ExpChatApp.Entity
{
    public class Conversation
    {
        public int ConversationId { get; set; }
        public string ConversationType { get; set; } = "Individual";
        public ICollection<ConversationMember> ConversationMembers { get; set; }= new List<ConversationMember>();
        public ICollection<Message> Messages { get; set; }= new List<Message>();
        public DateTime CreatedAt { get; set; }=DateTime.Now;
    }
}
