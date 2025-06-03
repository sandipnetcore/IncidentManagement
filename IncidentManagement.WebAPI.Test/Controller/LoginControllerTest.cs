using IncidentManagement.BusinessLogic.User;
using IncidentManagement.DataModel.User;
using IncidentManagement.WebAPI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentManagement.WebAPI.Test.Controller
{
    public class LoginControllerTest
    {

        private Mock<IUserRepository> _mockUserRepo;
        private LoginController _controller;


        [SetUp]
        public void Setup()
        {
            _mockUserRepo = new Mock<IUserRepository>();
            _controller = new LoginController(_mockUserRepo.Object);

        }

        [Test]
        public async Task GenerateToken_Expects_Token_Test()
        {
            // Arrange
            LoginCredentialModel model = new LoginCredentialModel()
            {
                Password = "TestPassword",
                UserName = "TestUser"
            };

            _mockUserRepo.Setup(m => m.GenerateToken(It.IsAny<LoginCredentialModel>()))
                .ReturnsAsync("TestToken");


            // Act 
            var result = await _controller.Post(model) as OkObjectResult;

            // Assert
            Assert.That(result?.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value?.ToString(), Does.Contain("TestToken"));
        }

        [Test]
        public async Task Login_Unauthorized_Test()
        {
            // Arrange
            LoginCredentialModel model = new LoginCredentialModel()
            {
                Password = "TestPassword",
                UserName = "TestUser"
            };

            _mockUserRepo.Setup(m => m.GenerateToken(It.IsAny<LoginCredentialModel>()))
                .ReturnsAsync(string.Empty);


            //trigger the ModelState.IsValid method of the controller.
            _controller.ControllerContext = new ControllerContext();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();

            ValidateModel(_controller as ControllerBase, model);

            // Act 
            var result = await _controller.Post(model) as StatusCodeResult;

            // Assert
            Assert.IsNotNull(result);

            Assert.IsTrue(result.StatusCode.Equals(401));
        }

        private void ValidateModel(ControllerBase controller, object model)
        {
            var validationContext = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(model, validationContext, validationResults, true);

            foreach (var validationResult in validationResults)
            {
                foreach (var memberName in validationResult.MemberNames)
                {
                    controller.ModelState.AddModelError(memberName, validationResult.ErrorMessage);
                }
            }
        }
    }
}
