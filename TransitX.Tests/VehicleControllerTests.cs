using Microsoft.AspNetCore.Mvc;
using Moq;
using TransitX.API.Controllers;
using TransitX.Common.Models;
using TransitX.Common.Service;

namespace TransitX.Tests
{
    [TestClass]
    public class VehicleControllerTests
    {
        private Mock<IService<Vehicle>> _mockService;
        private VehicleController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new Mock<IService<Vehicle>>();
            _controller = new VehicleController(_mockService.Object);
        }

        [TestMethod]
        public async Task TestGetById_ReturnsVehicle_WhenExists()
        {
            var testVehicle = new Vehicle { Id = "123", Type = "Bus", Capacity = 50 };
            _mockService.Setup(repo => repo.GetById("123")).ReturnsAsync(testVehicle);

            var actionResult = await _controller.GetById("123");

            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));
            var okResult = actionResult.Result as OkObjectResult;
            Assert.IsInstanceOfType(okResult.Value, typeof(Vehicle));
            var vehicle = okResult.Value as Vehicle;
            Assert.AreEqual(testVehicle.Id, vehicle.Id);
            Assert.AreEqual(testVehicle.Type, vehicle.Type);
            Assert.AreEqual(testVehicle.Capacity, vehicle.Capacity);
        }

        [TestMethod]
        public async Task TestGetById_ReturnsNotFound_WhenNotExists()
        {
            _mockService.Setup(repo => repo.GetById("123")).ReturnsAsync((Vehicle)null);

            var actionResult = await _controller.GetById("123");

            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task TestCreate_ReturnsCreatedResponse_OnSuccess()
        {
            var newVehicle = new Vehicle { Id = "789", Type = "Car", Capacity = 4 };
            _mockService.Setup(repo => repo.Insert(newVehicle)).Returns(Task.CompletedTask);

            var result = await _controller.Create(newVehicle);

            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            Assert.AreEqual("GetById", createdAtActionResult.ActionName);
            Assert.AreEqual(newVehicle.Id, createdAtActionResult.RouteValues["id"]);
            Assert.AreSame(newVehicle, createdAtActionResult.Value);
        }

        [TestMethod]
        public async Task TestUpdate_ReturnsNoContent_WhenUpdatedSuccessfully()
        {
            var updatedVehicle = new Vehicle { Id = "123", Type = "Bus", Capacity = 50 };
            _mockService.Setup(repo => repo.Update("123", updatedVehicle)).ReturnsAsync(true);

            var result = await _controller.Update("123", updatedVehicle);

            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task TestUpdate_ReturnsNotFound_WhenNoDocumentsUpdated()
        {
            _mockService.Setup(repo => repo.Update("123", It.IsAny<Vehicle>())).ReturnsAsync(false);

            var result = await _controller.Update("123", new Vehicle());

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task TestDelete_ReturnsNoContent_WhenDeletedSuccessfully()
        {
            _mockService.Setup(repo => repo.Delete("123")).ReturnsAsync(true);

            var result = await _controller.Delete("123");

            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task TestDelete_ReturnsNotFound_WhenNoDocumentsDeleted()
        {
            _mockService.Setup(repo => repo.Delete("123")).ReturnsAsync(false);

            var result = await _controller.Delete("123");

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task TestDeleteAll_ReturnsNoContent_WhenDeletedSuccessfully()
        {
            _mockService.Setup(repo => repo.DeleteAll()).ReturnsAsync(true);

            var result = await _controller.DeleteAll();

            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task TestDeleteAll_ReturnsNotFound_WhenNoDocumentsDeleted()
        {
            _mockService.Setup(repo => repo.DeleteAll()).ReturnsAsync(false);

            var result = await _controller.DeleteAll();

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
