namespace ExpChatApp.Entity
{
    public class Message
    {
        public int  MessageId { get; set; }
        public int  ConversationId { get; set; }
        public int  SenderId { get; set; }
        public DateTime  SentAt { get; set; }
     
        public required string Content { get; set; }
        public Conversation Conversation { get; set; }
        public User Sender { get; set; }

    }
}
