using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace WebChat.Models
{
    public class MessengerContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<PrivateChat> PrivateChats { get; set; }
        public DbSet<GroupChat> GroupChats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<PrivatelyDeletedMsg> PrivatelyDeletedMessages { get; set; }

        public MessengerContext(DbContextOptions<MessengerContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
