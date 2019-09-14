using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ServicesAccessibilityChecker.Controllers;
using Xunit;

namespace AccessibilityChecker.Test
{
    public class TestIndexController
    {
        [Fact]
        public void Get_ShouldReturnContentResult()
        {
            //Arrange
            Mock<ILogger<IndexController>> loggerMock = new Mock<ILogger<IndexController>>();
            IndexController indexController = new IndexController(loggerMock.Object);

            //Act
            var result = indexController.Get();
            
            //Assert
            Assert.IsType<ContentResult>(result);
        }
    }
}
