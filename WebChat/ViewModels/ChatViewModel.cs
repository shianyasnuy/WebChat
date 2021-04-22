using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebChat.Models;

namespace WebChat.ViewModels
{
    public class ChatViewModel
    {
        public List<Message> Messages { get; set; }
        public Chat Chat { get; set; }
        public User CurrUser { get; set; }
        public Message MessageToSend { get; set; }

        public ChatViewModel()
        {
            MessageToSend = new Message();
        }
    }
}
