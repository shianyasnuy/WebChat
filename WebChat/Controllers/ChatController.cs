using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebChat.Models;
using WebChat.Models.Forms;
using WebChat.ViewModels;

namespace WebChat.Controllers
{
    public class ChatController : Controller
    {
        private MessengerContext _context;

        public ChatController(MessengerContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Chats()
        {
            var chatIds = await _context.PrivateChats?
                .Where(x => x.User1.Id == SessionInfo.CurrUser.Id || x.User2.Id == SessionInfo.CurrUser.Id)?
                .Select(x => x.Chat.Id)?.ToListAsync();
            chatIds.AddRange(await _context.GroupChats?
                .Select(x => x.Chat.Id)?.ToListAsync());
            var chatsVM = new ChatsViewModel()
            {
                Chats = await _context.Chats?.Where(x => chatIds.Contains(x.Id))?.ToListAsync(),
                Users = await _context.Users?.Where(x => x.Id != SessionInfo.CurrUser.Id)?.ToListAsync()
            };
            return View(chatsVM);
        }

        public async Task<IActionResult> PrivateChat(int user1Id, int user2Id)
        {
            var users = new List<int> { user1Id, user2Id };
            var privChat = await _context.PrivateChats
                .Include(x => x.User1)
                .Include(x => x.User2)
                .Include(x => x.Chat)
                .FirstOrDefaultAsync(x => users.Contains(x.User1.Id) && users.Contains(x.User2.Id));

            if (privChat is null)
            {
                var chat = new Chat()
                {
                    ChatName = (await _context.Users.FindAsync(user1Id)).NickName+
                               "-"+(await _context.Users.FindAsync(user2Id)).NickName
                };

                privChat = new PrivateChat()
                {
                    Chat = chat,
                    User1 = await _context.Users.FindAsync(user1Id),
                    User2 = await _context.Users.FindAsync(user2Id)
                };

                await _context.Chats.AddAsync(chat);
                await _context.PrivateChats.AddAsync(privChat);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Chat", "Chat", new {chatId = privChat.Chat.Id});

        }

        public async Task<IActionResult> NewGroupChat(ChatsViewModel chatsVM)
        {
            var chat = new Chat()
            {
                ChatName = chatsVM.NewGroupChat.ChatName
            };

            var groupChat = new GroupChat()
            {
                Chat = chat,
                User = await _context.Users.FindAsync(SessionInfo.CurrUser.Id)
            };

            await _context.Chats.AddAsync(chat);
            await _context.GroupChats.AddAsync(groupChat);
            await _context.SaveChangesAsync();

            return RedirectToAction("Chat", "Chat", new { chatId = chat.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Chat(int chatId)
        {
            var retChat = new ChatViewModel()
            {
                Messages = await _context.Messages.Where(x => x.Chat.Id == chatId)
                    .Include(x => x.User)
                    .Include(x => x.MessageToReply.User).ToListAsync(),
                Chat = _context.Chats.FirstOrDefault(x => x.Id == chatId),
                CurrUser = SessionInfo.CurrUser
            };
            
            var deletedMsg = await _context.PrivatelyDeletedMessages
                .Include(x => x.User)
                .Where(x => x.User.Id == SessionInfo.CurrUser.Id)
                .ToListAsync();
            
            if (deletedMsg.Count != 0)
            {
                var del = deletedMsg.Select(x=>x.Message);
                retChat.Messages = retChat.Messages.Where(x => !del.Contains(x)).ToList();
            }

            retChat.Messages.Reverse();
            retChat.Messages = retChat.Messages.Take(20).Reverse().ToList();

            return View(retChat);
        }

        public async Task<IActionResult> FetchNext(FetchForm fetch)
        {

            var Messages = await _context.Messages.Where(x => x.Chat.Id == fetch.ChatId)
                .Include(x => x.User)
                .Include(x => x.MessageToReply.User).ToListAsync();

            var deletedMsg = await _context.PrivatelyDeletedMessages
                .Include(x => x.User)
                .Where(x => x.User.Id == SessionInfo.CurrUser.Id)
                .ToListAsync();

            if (deletedMsg.Count != 0)
            {
                var del = deletedMsg.Select(x => x.Message);
                Messages = Messages.Where(x => !del.Contains(x)).ToList();
            }

            Messages.Reverse();
            return Json(new { Messages = Messages.Skip((20 * fetch.FetchOpNo)+fetch.MessagesAdded).Take(20).ToList()});
        }
    }
}
