using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebChat.Models.Forms
{
    public class FetchForm
    {
        public int ChatId { get; set; }
        public int FetchOpNo { get; set; }
        public int MessagesAdded { get; set; }
    }
}
