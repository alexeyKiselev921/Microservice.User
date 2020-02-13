using System.Threading.Tasks;
using Microservice.User.Service.Controllers;
using Microservice.User.Service.Models;
using Microservice.User.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Microservice.User.Tests
{
    [TestClass]
    public class CreateUserControllerTests
    {
        [TestMethod]
        public async Task IsUser_Valid_ReturnOk()
        {
            var user = new UserModel()
            {
                Username = "Test",
                Password = "Test",
                Email = "test@test.com"
            };
            var mockRepository = new Mock<IUserService>();
            mockRepository.Setup(repo => repo.Create(user));

            var controller = new UserController(mockRepository.Object);

            var result = await controller.Post(user);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task IsUser_UsernameInvalid_ReturnBadRequest()
        {
            var user = new UserModel()
            {
                Password = "Test",
                Email = "test@test.com"
            };

            var mockRepository = new Mock<IUserService>();
            mockRepository.Setup(repo => repo.Create(user));

            var controller = new UserController(mockRepository.Object);

            var result = await controller.Post(user);

            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task IsUser_PasswordInvalid_ReturnBadRequest()
        {
            var user = new UserModel()
            {
                Username = "Test",
                Email = "test@test.com"
            };

            var mockRepository = new Mock<IUserService>();
            mockRepository.Setup(repo => repo.Create(user));

            var controller = new UserController(mockRepository.Object);

            var result = await controller.Post(user);

            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task IsUser_EmailInvalid_ReturnBadRequest()
        {
            var user = new UserModel()
            {
                Username = "Test",
                Password = "Test",
                Email = "test.com"
            };

            var mockRepository = new Mock<IUserService>();
            mockRepository.Setup(repo => repo.Create(user));

            var controller = new UserController(mockRepository.Object);

            var result = await controller.Post(user);

            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }
    }
}
