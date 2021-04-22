using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebChat.Models
{
    public class MessageForm
    {
        public string Text { get; set; }
        public int MessageId { get; set; }
        public int ChatId { get; set; }
        public int UserId { get; set; }
        public int MessageToReplyId { get; set; }
    }
}
