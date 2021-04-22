using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebChat.Models
{
    public class RegForm
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string RepPassword { get; set; }
    }
}
