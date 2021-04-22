using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using WebChat.Controllers;
using WebChat.Models;

namespace WebChatTests.ControllerTests
{
    public class UserControllerTest
    {
        public UserController userController;

        [SetUp]
        public void Setup()
        {
            var connStr = @"Data Source=webchatserver.database.windows.net;Initial Catalog=WebChatDB;User ID=shianyasnuy;Password=18Q02q2001;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            var optionsBuilder = new DbContextOptionsBuilder<MessengerContext>();
            optionsBuilder.UseSqlServer(connStr);
            userController = new UserController(new MessengerContext(optionsBuilder.Options));
        }

        [Test]
        public void LogInUnexistingTest()
        {
            Task.Run(async () => {
                var taskResult = await userController.LogIn(new User
                {
                    NickName = "UnexistingNickName228",
                    Password = "ababahalamaga"
                });
                var result = taskResult as RedirectToActionResult;
                Assert.AreEqual("Index", result.ActionName);
            }).GetAwaiter().GetResult();
        }

        [Test]
        public async Task LogInExistingTest()
        {
            Task.Run(async () => {
                var taskResult = await userController.LogIn(new User
                {
                    NickName = "Shian",
                    Password = "12345"
                });
                var result = taskResult as RedirectToActionResult;
                Assert.AreEqual("Chats", result.ActionName);
            }).GetAwaiter().GetResult();
        }
    }
}
