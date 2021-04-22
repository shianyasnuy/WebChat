using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebChat.Models;

namespace WebChat.Controllers
{
    public class HomeController : Controller
    {
        private MessengerContext _context;

        public HomeController(MessengerContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (SessionInfo.CurrUser != null)
            {
                return RedirectToAction("Chats", "Chat");
            }
            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }

        public IActionResult LogOut()
        {
            SessionInfo.CurrUser = null;
            return RedirectToAction("Index");
        }
    }
}
