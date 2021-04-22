using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using WebChat.Controllers;
using WebChat.Models;
using WebChat.ViewModels;

namespace WebChatTests.ControllerTests
{
    class ChatControllerTest
    {
        public ChatController chatController;

        [SetUp]
        public void Setup()
        {
            var connStr = @"Data Source=webchatserver.database.windows.net;Initial Catalog=WebChatDB;User ID=shianyasnuy;Password=18Q02q2001;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            var optionsBuilder = new DbContextOptionsBuilder<MessengerContext>();
            optionsBuilder.UseSqlServer(connStr);
            chatController = new ChatController(new MessengerContext(optionsBuilder.Options));
            
            SessionInfo.CurrUser = new User
            {
                Id = 1,
                NickName = "Shian",
                Password = "12345"
            };
        }

        [Test]
        public void ChatsTest()
        {
            Task.Run(async () =>
            {
                var taskResult = await chatController.Chats();
                var result = taskResult as ViewResult;
                Assert.IsInstanceOf<ChatsViewModel>(result.Model);
            }).GetAwaiter().GetResult();
        }

        [Test]
        public void PrivateChatTest()
        {
            Task.Run(async () =>
            {
                var taskResult = await chatController.PrivateChat(1,2);
                var result = taskResult as RedirectToActionResult;
                Assert.AreEqual(4, result.RouteValues["chatId"]);
            }).GetAwaiter().GetResult();
        }

        [Test]
        public void ChatTest()
        {
            Task.Run(async () =>
            {
                var taskResult = await chatController.Chat(2);
                var result = taskResult as ViewResult;
                Assert.IsInstanceOf<ChatViewModel>(result.Model);
            }).GetAwaiter().GetResult();
        }
    }
}
