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
            Mock<ILogger<IndexController>> loggerMock = new Mock<ILogger<IndexController>>();

            IndexController indexController = new IndexController(loggerMock.Object);

            var result = indexController.Get();
            Assert.IsType<ContentResult>(result);
        }
    }
}
