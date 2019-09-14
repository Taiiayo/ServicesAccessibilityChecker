using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ServicesAccessibilityChecker.Controllers;
using ServicesAccessibilityChecker.Models;
using System.Threading.Tasks;
using Xunit;

namespace AccessibilityChecker.Test
{
    public class TestStatusController
    {
        [Theory]
        [InlineData(3)]
        public async Task GetStatusAsync_ShouldReturnBadRequest(int serviceId)
        {
            //Arrange
            Mock<ILogger<StatusController>> loggerMock = new Mock<ILogger<StatusController>>();
            Mock<IStatusChecker> statusCheckerMock = new Mock<IStatusChecker>();
            Mock<IFullInfo> fullInfoMock = new Mock<IFullInfo>();
            StatusController statusController = new StatusController(fullInfoMock.Object, loggerMock.Object, statusCheckerMock.Object);

            //Act
            var result = await statusController.GetStatusAsync(serviceId);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Theory]
        [InlineData(3)]
        public void GetFullStatusAsync_ShouldReturnBadRequest(int serviceId)
        {
            //Arrange
            Mock<ILogger<StatusController>> loggerMock = new Mock<ILogger<StatusController>>();
            Mock<IStatusChecker> statusCheckerMock = new Mock<IStatusChecker>();
            Mock<IFullInfo> fullInfoMock = new Mock<IFullInfo>();
            StatusController statusController = new StatusController(fullInfoMock.Object, loggerMock.Object, statusCheckerMock.Object);

            //Act
            var result = statusController.GetFullStatusAsync(serviceId);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
    }
}
