using Fit.Controllers;
using FitCore.Dto.Search;
using FitCore.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Fit.Tests
{
    public class SearchController_GetAllTrainees_Tests
    {

        // GetAllTrainess Tests 2 scenarios
        [Fact]
        public async Task GetAllTrainees_WithTraineesExist_ReturnsOkResultWithList()
        {
            // Arrange
            var trainees = new List<GetTrainee>
            {
                new GetTrainee { Id = "1", FirstName = "Ahmed", LastName = "Mohamed" },
                new GetTrainee { Id = "2", FirstName = "Ali", LastName = "Kamal" }
            };

            var mockSearchRepo = new Mock<ISearch>();
            mockSearchRepo.Setup(s => s.GetAllTrainees()).ReturnsAsync(trainees);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(u => u.Search).Returns(mockSearchRepo.Object);

            var controller = new SearchController(mockUnitOfWork.Object);

            // Act
            var result = await controller.GetAllTrainees();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<GetTrainee>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetAllTrainees_WithNoTrainees_ReturnsOkResultWithMessage()
        {
            // Arrange
            var emptyTrainees = new List<GetTrainee>();

            var mockSearchRepo = new Mock<ISearch>();
            mockSearchRepo.Setup(s => s.GetAllTrainees()).ReturnsAsync(emptyTrainees);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(u => u.Search).Returns(mockSearchRepo.Object);

            var controller = new SearchController(mockUnitOfWork.Object);

            // Act
            var result = await controller.GetAllTrainees();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("No Trainees Found", okResult.Value);
        }

        // GetAllTrainers 2 scenarios
        [Fact]
        public async Task GetAllTrainers_WithTrainersExist_ReturnsOkResultWithList()
        {
            // Arrange
            var trainers = new List<GetNutritionistsAndTrainers>
            {
                new GetNutritionistsAndTrainers { Id = "1", FirstName = "Ali", LastName = "Ahmed", ExYreas = 5 },
                new GetNutritionistsAndTrainers { Id = "2", FirstName = "Mahmoud", LastName = "Kamal", ExYreas = 3 }
            };

            var mockSearchRepo = new Mock<ISearch>();
            mockSearchRepo.Setup(s => s.GetAllTrainers()).ReturnsAsync(trainers);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(u => u.Search).Returns(mockSearchRepo.Object);

            var controller = new SearchController(mockUnitOfWork.Object);

            // Act
            var result = await controller.GetAllTrainers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<GetNutritionistsAndTrainers>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetAllTrainers_WithNoTrainers_ReturnsNotFound()
        {
            // Arrange
            var emptyList = new List<GetNutritionistsAndTrainers>();

            var mockSearchRepo = new Mock<ISearch>();
            mockSearchRepo.Setup(s => s.GetAllTrainers()).ReturnsAsync(emptyList);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(u => u.Search).Returns(mockSearchRepo.Object);

            var controller = new SearchController(mockUnitOfWork.Object);

            // Act
            var result = await controller.GetAllTrainers();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("No Trainers Found", notFoundResult.Value);
        }

        // GetAllNutritionist 2 scenarios
        [Fact]
        public async Task GetAllNutritionist_WithNutritionistsExist_ReturnsOkResultWithList()
        {
            // Arrange
            var nutritionists = new List<GetNutritionistsAndTrainers>
            {
                new GetNutritionistsAndTrainers { Id = "1", FirstName = "Dr. Mona", LastName = "Ali", ExYreas = 7 },
                new GetNutritionistsAndTrainers { Id = "2", FirstName = "Dr. Sarah", LastName = "Kamal", ExYreas = 4 }
            };

            var mockSearchRepo = new Mock<ISearch>();
            mockSearchRepo.Setup(s => s.GetAllNutritionist()).ReturnsAsync(nutritionists);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(u => u.Search).Returns(mockSearchRepo.Object);

            var controller = new SearchController(mockUnitOfWork.Object);

            // Act
            var result = await controller.GetAllNutritionist();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<GetNutritionistsAndTrainers>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetAllNutritionist_WithNoNutritionists_ReturnsNotFound()
        {
            // Arrange
            var emptyList = new List<GetNutritionistsAndTrainers>();

            var mockSearchRepo = new Mock<ISearch>();
            mockSearchRepo.Setup(s => s.GetAllNutritionist()).ReturnsAsync(emptyList);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(u => u.Search).Returns(mockSearchRepo.Object);

            var controller = new SearchController(mockUnitOfWork.Object);

            // Act
            var result = await controller.GetAllNutritionist();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("No Nutritionists Found", notFoundResult.Value);
        }

        // GetByName 3 scenarios
        [Fact]
        public async Task GetUserByName_WhenNameIsEmpty_ReturnsBadRequest()
        {
            // Arrange
            var mockSearchRepo = new Mock<ISearch>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(u => u.Search).Returns(mockSearchRepo.Object);

            var controller = new SearchController(mockUnitOfWork.Object);

            // Act
            var result = await controller.GetUserByName("");

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Name is required.", badRequestResult.Value);
        }

        [Fact]
        public async Task GetUserByName_WithNoUsersFound_ReturnsNotFound()
        {
            // Arrange
            var emptyList = new List<GetTrainee>();

            var mockSearchRepo = new Mock<ISearch>();
            mockSearchRepo.Setup(s => s.SearchUserByName("Ali")).ReturnsAsync(emptyList);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(u => u.Search).Returns(mockSearchRepo.Object);

            var controller = new SearchController(mockUnitOfWork.Object);

            // Act
            var result = await controller.GetUserByName("Ali");

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("User not found with name Ali", notFoundResult.Value);
        }

        [Fact]
        public async Task GetUserByName_WithUsersFound_ReturnsOkResultWithList()
        {
            // Arrange
            var users = new List<GetTrainee>
            {
                new GetTrainee { Id = "1", FirstName = "Ahmed", LastName = "Ali" },
                new GetTrainee { Id = "2", FirstName = "Ali", LastName = "Mohamed" }
            };

            var mockSearchRepo = new Mock<ISearch>();
            mockSearchRepo.Setup(s => s.SearchUserByName("Ali")).ReturnsAsync(users);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(u => u.Search).Returns(mockSearchRepo.Object);

            var controller = new SearchController(mockUnitOfWork.Object);

            // Act
            var result = await controller.GetUserByName("Ali");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<GetTrainee>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        // GetById 2 scenarios
        [Fact]
        public async Task GetUserById_WithValidId_ReturnsOkResult()
        {
            // Arrange
            var user = new GetTrainee
            {
                Id = "1",
                FirstName = "Ahmed",
                LastName = "Mohamed"
            };

            var mockSearchRepo = new Mock<ISearch>();
            mockSearchRepo.Setup(s => s.SearchUserById("1")).ReturnsAsync(user);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(u => u.Search).Returns(mockSearchRepo.Object);

            var controller = new SearchController(mockUnitOfWork.Object);

            // Act
            var result = await controller.GetUserById("1");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<GetTrainee>(okResult.Value);
            Assert.Equal("Ahmed", returnValue.FirstName);
        }

        [Fact]
        public async Task GetUserById_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var mockSearchRepo = new Mock<ISearch>();
            mockSearchRepo.Setup(s => s.SearchUserById("999")).ReturnsAsync((GetTrainee)null);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(u => u.Search).Returns(mockSearchRepo.Object);

            var controller = new SearchController(mockUnitOfWork.Object);

            // Act
            var result = await controller.GetUserById("999");

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("User not found with ID = 999", notFoundResult.Value);
        }
    }
}
