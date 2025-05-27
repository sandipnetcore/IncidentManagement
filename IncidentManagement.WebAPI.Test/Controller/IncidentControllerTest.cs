using IncidentManagement.BusinessLogic.Incident;
using IncidentManagement.DataModel.UIModels.UIIncident;
using IncidentManagement.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentManagement.WebAPI.Test.Controller
{
    public class IncidentControllerTest
    {
        private IncidentController _incidentController;
        private Mock<IIncidentRepository> _mockIncidentRepository;

        [SetUp]
        public void Setup()
        {
            _mockIncidentRepository = new Mock<IIncidentRepository>();
            _incidentController = new IncidentController(_mockIncidentRepository.Object);
        }

        [Test]
        public async Task GetAllIncidents_ShouldReturnOkResult_Test()
        {

            // Arrange
            List<UIIncidentModel> incidents = new List<UIIncidentModel>
            {
                new UIIncidentModel { IncidentId = "1", IncidentTitle = "Test Incident 1" },
                new UIIncidentModel { IncidentId = "2", IncidentTitle = "Test Incident 2" }
            };

            _mockIncidentRepository.Setup(repo => repo.GetAllIncidents())
                .ReturnsAsync(incidents);

            // Act
            var result = await _incidentController.Get() as OkObjectResult;
            
            
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result?.StatusCode);
        }
    }
}
