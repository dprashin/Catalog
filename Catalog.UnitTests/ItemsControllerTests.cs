using System;
using Xunit;
using Moq;
using System.Threading.Tasks;
using Catalog.Api.Repositories;
using Catalog.Api.Entities;
using Microsoft.Extensions.Logging;
using Catalog.Api.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.UnitTests
{
    public class ItemsControllerTests
    {
        [Fact]
        public async Task GetItemAsync_WithUnexistingItem_ReturnsNotFound()
        {
            //Arrange
            var repositoryStub = new Mock<IItemsRepository>();
            repositoryStub.Setup(repo => repo.GetItemAsync(It.IsAny<Guid>()))
                        .ReturnsAsync((Item)null);
            var loggerStub = new Mock<ILogger<ItemsController>>();
            var controller = new ItemsController(repositoryStub.Object, loggerStub.Object);

            //Act
            var result = await controller.GetItemAsync(Guid.NewGuid());

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);

        }
    }
}
