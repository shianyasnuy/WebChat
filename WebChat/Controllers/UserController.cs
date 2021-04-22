using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WebChat.Models;

namespace WebChat.Controllers
{
    public class UserController : Controller
    {

        private MessengerContext _context;

        public UserController(MessengerContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(User user)
        {
            var foundUser = await _context.Users.FirstOrDefaultAsync(x =>
                x.NickName == user.NickName && x.Password == user.Password);
            if (foundUser is null)
            {
                TempData?.Add("FailMessage", "Wrong password for this user, or user does not exist.");
                return RedirectToAction("Index", "Home");
            }

            if(TempData != null)
                TempData["CurrUser"] = foundUser.Id;
            SessionInfo.CurrUser = foundUser;
            return RedirectToAction("Chats", "Chat");
        }

        [HttpPost]
        public async Task<IActionResult> Register(string userName, string password, string repPassword)
        {
            if (password != repPassword)
            {
                TempData.Add("FailRegMessage", "Password fields do not match.");
                return RedirectToAction("Registration", "Home");
            }

            var user = new User()
            {
                NickName = userName,
                Password = password
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Chats", "Chat");
        }
    }
}
