using ExpChatApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace ExpChatApp.Data
{
    public class ChatAppDbContext:DbContext
    {
        public ChatAppDbContext(DbContextOptions<ChatAppDbContext> options) : base(options) { }

        public DbSet<User> users { get; set; }
        public DbSet<Conversation> conversations { get; set; }
        public DbSet<ConversationMember> conversation_members { get; set; }
        public DbSet<Message> messages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConversationMember>()
                .HasKey(cm => new
                {
                    cm.ConversationId,
                    cm.UserId
                });

            modelBuilder.Entity<ConversationMember>()
                .HasOne(cm => cm.Conversation)
                .WithMany(c => c.ConversationMembers)
                .HasForeignKey(cm => cm.ConversationId);

            modelBuilder.Entity<ConversationMember>()
                .HasOne(cm => cm.User)
                .WithMany(u => u.ConversationMembers)
                .HasForeignKey(cm => cm.UserId);
            // Message -> Conversation
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Conversation)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ConversationId);

            // Message -> User
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.Messages)
                .HasForeignKey(m => m.SenderId);
        }

    }
}
