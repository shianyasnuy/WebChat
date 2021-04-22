using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebChat.Models
{
    public class PrivatelyDeletedMsg
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Message Message { get; set; }
    }
}
