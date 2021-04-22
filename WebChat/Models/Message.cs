#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebChat.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }
        public Chat Chat { get; set; }
        public User User { get; set; }
        public Message? MessageToReply { get; set; }
    }
}
