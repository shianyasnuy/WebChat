﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebChat.Models
{
    public class User
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
    }
}
