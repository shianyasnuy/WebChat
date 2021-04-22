using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebChat.Models;

namespace WebChat.Controllers
{
    public class MessageController : Controller
    {
        private MessengerContext _context;

        public MessageController(MessengerContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> SendMessage(MessageForm messageForm)
        {
            var message = new Message()
            {
                Text = messageForm.Text,
                Chat = await _context.Chats.FindAsync(messageForm.ChatId),
                User = await _context.Users.FindAsync(messageForm.UserId),
                Time = DateTime.Now
            };

            if (messageForm.MessageToReplyId != 0)
            {
                message.MessageToReply = await _context.Messages.FindAsync(messageForm.MessageToReplyId);
            }

            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
            return Json(new {sentId = message.Id});
        }

        public async Task<IActionResult> ReplyPrivate(MessageForm messageForm)
        {
            var message = new Message()
            {
                Text = messageForm.Text,
                User = await _context.Users.FindAsync(messageForm.UserId),
                MessageToReply = await _context.Messages.FindAsync(messageForm.MessageToReplyId),
                Time = DateTime.Now
            };

            var detailMessage = (await _context.Messages
                .Where(x => x.Id == messageForm.MessageToReplyId)
                .Include(m => m.User).ToListAsync())[0];

            var conv = new List<User> { detailMessage.User, message.User };

            var privChatList = await _context.PrivateChats
                .Where(x => conv.Contains(x.User1) && conv.Contains(x.User2))
                .Include(c => c.Chat).ToListAsync();

            PrivateChat privChat = null;
            if (privChatList.Count != 0)
                privChat = privChatList[0];

            if (privChat is null)
            {
                var chat = new Chat()
                {
                    ChatName = conv[0].NickName + "-" + conv[1].NickName
                };

                privChat = new PrivateChat()
                {
                    Chat = chat,
                    User1 = conv[0],
                    User2 = conv[1]
                };

                await _context.Chats.AddAsync(chat);
                await _context.PrivateChats.AddAsync(privChat);
            }

            message.Chat = privChat.Chat;
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();


            return Json(new { redirectToUrl = Url.Action("Chat", "Chat", 
                new {chatId = message.Chat.Id}) });
        }

        public async Task<IActionResult> Edit(MessageForm message)
        {
            var msgToEdit = await _context.Messages.FindAsync(message.MessageId);
            msgToEdit.Text = message.Text;
            await _context.SaveChangesAsync();
            return Ok();
        }

        public async Task<IActionResult> Delete(int messageId)
        {
            _context.Messages.Remove(await _context.Messages.FindAsync(messageId));
            await _context.SaveChangesAsync();
            return Ok();
        }

        public async Task<IActionResult> DeletePrivate(int messageId)
        {
            await _context.Database
                .ExecuteSqlRawAsync($"insert into dbo.PrivatelyDeletedMessages (UserId, MessageId)" +
                                                         $"values ({SessionInfo.CurrUser.Id}, {messageId})");

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
