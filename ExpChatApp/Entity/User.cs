namespace ExpChatApp.Entity
{
    public class User
    {
        public int UserId { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string HashPassword { get; set; }
        public ICollection<ConversationMember> ConversationMembers { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
