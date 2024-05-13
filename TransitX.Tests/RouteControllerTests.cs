using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Moq;
using TransitX.API.Controllers;
using TransitX.Common.Models;
using TransitX.Common.Repository;

namespace TransitX.Tests
{
    [TestClass]
    public class RouteControllerTests
    {
        private Mock<IRepository<Route>> _mockRepository;
        private RouteController _controller;

        [TestInitialize]
        public void Setup()
        {
            // Arrange
            _mockRepository = new Mock<IRepository<Route>>();
            _controller = new RouteController(_mockRepository.Object);
        }

        [TestMethod]
        public async Task TestGetAllRoutes_ReturnsListOfRoutes()
        {
            // Arrange
            var testRoutes = new List<Route>
            {
                new Route {Id = "123", StartLocation = new Coordinate(1.0, 2.0), EndLocation = new Coordinate(3.0, 4.0)},
                new Route {Id = "456", StartLocation = new Coordinate(5.0, 6.0), EndLocation = new Coordinate(7.0, 8.0)}
            };
            _mockRepository.Setup(repo => repo.GetPage(1, 10)).ReturnsAsync(testRoutes);

            // Act
            var actionResult = await _controller.Get();
            var okObjectResult = actionResult.Result as OkObjectResult;
            var routes = okObjectResult.Value as IEnumerable<Route>;

            // Assert
            Assert.IsNotNull(routes);
            Assert.AreEqual(testRoutes.Count, routes.Count());
        }

        [TestMethod]
        public async Task TestGetById_ReturnsRoute_WhenExists()
        {
            // Arrange
            var testRoute = new Route { Id = "123", StartLocation = new Coordinate(1.0, 2.0), EndLocation = new Coordinate(3.0, 4.0) };
            _mockRepository.Setup(repo => repo.GetById("123")).ReturnsAsync(testRoute);

            // Act
            var actionResult = await _controller.GetById("123");

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));
            var okResult = (OkObjectResult)actionResult.Result;
            Assert.IsInstanceOfType(okResult.Value, typeof(Route));
            var route = (Route)okResult.Value;
            Assert.AreEqual(testRoute.Id, route.Id);
            Assert.AreEqual(testRoute.StartLocation, route.StartLocation);
            Assert.AreEqual(testRoute.EndLocation, route.EndLocation);
        }

        [TestMethod]
        public async Task TestCreate_ReturnsCreatedResponse_OnSuccess()
        {
            // Arrange
            var newRoute = new Route { Id = "789", StartLocation = new Coordinate(9.0, 10.0), EndLocation = new Coordinate(11.0, 12.0) };
            _mockRepository.Setup(repo => repo.Insert(newRoute)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Create(newRoute);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
            var createdAtActionResult = (CreatedAtActionResult)result.Result;
            Assert.AreEqual("GetById", createdAtActionResult.ActionName);
            Assert.AreEqual(newRoute.Id, createdAtActionResult.RouteValues["id"]);
            Assert.AreSame(newRoute, createdAtActionResult.Value);
        }

        [TestMethod]
        public async Task TestUpdate_ReturnsNoContent_WhenUpdatedSuccessfully()
        {
            // Arrange
            var updatedRoute = new Route { Id = "123", StartLocation = new Coordinate(1.0, 2.0), EndLocation = new Coordinate(3.0, 4.0) };
            _mockRepository.Setup(repo => repo.Update("123", updatedRoute)).ReturnsAsync(true);

            // Act
            var result = await _controller.Update("123", updatedRoute);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task TestDelete_ReturnsNoContent_WhenDeletedSuccessfully()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.Delete("123")).ReturnsAsync(true);

            // Act
            var result = await _controller.Delete("123");

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task TestDeleteAll_ReturnsNoContent_WhenDeletedSuccessfully()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.DeleteAll()).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteAll();

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task TestDeleteAll_ReturnsNotFound_WhenNoDocumentsDeleted()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.DeleteAll()).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteAll();

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
