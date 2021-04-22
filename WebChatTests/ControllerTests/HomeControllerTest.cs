using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using WebChat.Controllers;
using WebChat.Models;


namespace WebChatTests
{
    public class HomeControllerTests
    {
        public HomeController homeController;

        [SetUp]
        public void Setup()
        {
            var connStr = @"Data Source=webchatserver.database.windows.net;Initial Catalog=WebChatDB;User ID=shianyasnuy;Password=18Q02q2001;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            var optionsBuilder = new DbContextOptionsBuilder<MessengerContext>();
            optionsBuilder.UseSqlServer(connStr);
            homeController = new HomeController(new MessengerContext(optionsBuilder.Options));
        }

        [Test]
        public void LogOutTest()
        {
            var result = homeController.LogOut() as RedirectToActionResult;
            Assert.AreEqual("Index", result.ActionName);
        }
    }
}