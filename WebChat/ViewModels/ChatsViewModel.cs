using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebChat.Models;

namespace WebChat.ViewModels
{
    public class ChatsViewModel
    {
        public List<Chat> Chats { get; set; }
        public List<User> Users { get; set; }
        public Chat NewGroupChat { get; set; }
    }
}
