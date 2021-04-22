using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebChat.Models
{
    public class PrivateChat
    {
        public int Id { get; set; }
        public Chat Chat { get; set; }
        public User User1 { get; set; }
        public User User2 { get; set; }
    }
}
