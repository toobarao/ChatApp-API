namespace ExpChatApp.Models
{
    public class ChatDto
    {
        public int ConvId { get; set; }
        public string Name { get; set; }
        public string LastMsg { get; set; }
        public List<int> members { get; set; }
        public DateTime? Date { get; set; }
    }
}
