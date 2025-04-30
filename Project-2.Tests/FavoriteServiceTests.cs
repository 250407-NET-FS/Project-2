using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Project_2.Models;
using Project_2.Services;
using Project_2.Data;
using Project_2.Models.DTOs;
using Project_2.Services.Services;

namespace Project_2.Tests
{
    public class FavoriteServiceTests
    {
        private readonly Mock<IFavoriteRepository> _favoriteRepositoryMock;
        private readonly Mock<IPropertyRepository> _propertyRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly FavoriteService _favoriteService;

        public FavoriteServiceTests()
        {
            _favoriteRepositoryMock = new Mock<IFavoriteRepository>();
            _propertyRepositoryMock = new Mock<IPropertyRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();

            _favoriteService = new FavoriteService(
                _favoriteRepositoryMock.Object,
                _propertyRepositoryMock.Object,
                _userRepositoryMock.Object
            );
        }

        // Test for AddAsync method with valid data
        [Fact]
        public async Task AddAsync_ShouldAddFa_AndHandleErrors()
        {
            // Arrange valid data
            FavoritesAddDTO favoriteDto = new FavoritesAddDTO
            {
                PropertyId = Guid.NewGuid(),
                UserId = Guid.NewGuid()
            };

            Property property = new Property("CountryName", "StateName", "CityName", "StreetName", "ZipCode", 1000.0m, 3, 500.0m)
            {
                PropertyID = favoriteDto.PropertyId
            };
            // Important: IdentityUser expects string ID, so use .ToString()
            User user = new User
            {
                Id = favoriteDto.UserId,
                UserName = "TestUser",
                Email = "testuser@example.com"
            };

            _propertyRepositoryMock.Setup(x => x.GetByIdAsync(favoriteDto.PropertyId)).ReturnsAsync(property);
            _userRepositoryMock.Setup(x => x.GetByIdAsync(favoriteDto.UserId)).ReturnsAsync(user);

            _favoriteRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Favorite>())).Returns(Task.CompletedTask);
            _favoriteRepositoryMock.Setup(x => x.SaveChangesAsync()).Returns(Task.FromResult(1));

            // Act & Assert for valid data
            var result = await _favoriteService.AddAsync(favoriteDto);
            Assert.NotNull(result);
            Assert.Equal(favoriteDto.PropertyId, result.PropertyId);
            Assert.Equal(favoriteDto.UserId, result.UserId);

            // Test for property not found
            _propertyRepositoryMock.Setup(x => x.GetByIdAsync(favoriteDto.PropertyId)).ReturnsAsync((Property)null);
            var exception = await Assert.ThrowsAsync<Exception>(() => _favoriteService.AddAsync(favoriteDto));
            Assert.Equal("Property cannot be null", exception.Message);

            // Test for user not found
            _propertyRepositoryMock.Setup(x => x.GetByIdAsync(favoriteDto.PropertyId)).ReturnsAsync(property);
            _userRepositoryMock.Setup(x => x.GetByIdAsync(favoriteDto.UserId)).ReturnsAsync((User)null);
            exception = await Assert.ThrowsAsync<Exception>(() => _favoriteService.AddAsync(favoriteDto));
            Assert.Equal("User cannot be null", exception.Message);
        }

        // Test for RemoveAsync method
        [Fact]
        public async Task RemoveAsync_ShouldRemoveFavorite_AndHandleErrors()
        {
            // Arrange
            Guid favoriteId = Guid.NewGuid();
            Favorite favorite = new Favorite(Guid.NewGuid(), Guid.NewGuid()) { FavoriteID = favoriteId };

            _favoriteRepositoryMock.Setup(x => x.GetByIdAsync(favoriteId)).ReturnsAsync(favorite);
            _favoriteRepositoryMock.Setup(x => x.Remove(It.IsAny<Favorite>()));

            // Act & Assert for valid data
            await _favoriteService.RemoveAsync(favoriteId);

            // Assert Remove method is called
            _favoriteRepositoryMock.Verify(x => x.Remove(It.IsAny<Favorite>()), Times.Once);

            // Test for Favorite not found
            _favoriteRepositoryMock.Setup(x => x.GetByIdAsync(favoriteId)).ReturnsAsync((Favorite)null);
            var exception = await Assert.ThrowsAsync<Exception>(() => _favoriteService.RemoveAsync(favoriteId));
            Assert.Equal("Favorite not found", exception.Message);
        }

        // Test for GetByIdAsync method
        [Fact]
        public async Task GetByIdAsync_ShouldReturnFavorite_AndHandleErrors()
        {
            // Arrange
            Guid favoriteId = Guid.NewGuid();
            Favorite favorite = new Favorite(Guid.NewGuid(), Guid.NewGuid()) { FavoriteID = favoriteId };

            _favoriteRepositoryMock.Setup(x => x.GetByIdAsync(favoriteId)).ReturnsAsync(favorite);

            // Act & Assert for valid data
            var result = await _favoriteService.GetByIdAsync(favoriteId);
            Assert.NotNull(result);
            Assert.Equal(favoriteId, result.FavoriteId);

            // Test for favorite not found
            _favoriteRepositoryMock.Setup(x => x.GetByIdAsync(favoriteId)).ReturnsAsync((Favorite)null);
            var exception = await Assert.ThrowsAsync<Exception>(() => _favoriteService.GetByIdAsync(favoriteId));
            Assert.Equal("Favorite not found", exception.Message);
        }

        // Test for GetAllForProperty method
        [Fact]
        public async Task GetAllForProperty_ShouldReturnFavorites_AndHandleErrors()
        {
            // Arrange
            Guid propertyId = Guid.NewGuid();
            List<Favorite> favorites = new List<Favorite>
            {
                new Favorite(Guid.NewGuid(), Guid.NewGuid()),
                new Favorite(Guid.NewGuid(), Guid.NewGuid())
            };

            _propertyRepositoryMock.Setup(x => x.GetByIdAsync(propertyId)).ReturnsAsync(new Property("CountryName", "StateName", "CityName", "StreetName", "ZipCode", 1000.0m, 3, 500.0m) { PropertyID = propertyId });
            _favoriteRepositoryMock.Setup(x => x.GetAllForProperty(propertyId)).ReturnsAsync(favorites);

            // Act & Assert for valid data
            var result = await _favoriteService.GetAllForProperty(propertyId);
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());

            // Test for property not found
            _propertyRepositoryMock.Setup(x => x.GetByIdAsync(propertyId)).ReturnsAsync((Property)null);
            var exception = await Assert.ThrowsAsync<Exception>(() => _favoriteService.GetAllForProperty(propertyId));
            Assert.Equal("Property does not exist", exception.Message);
        }

        // Test for GetAllByUser method
        [Fact]
        public async Task GetAllByUser_ShouldReturnFavorites_AndHandleErrors()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            List<Favorite> favorites = new List<Favorite>
            {
                new Favorite(Guid.NewGuid(), userId),
                new Favorite(Guid.NewGuid(), userId)
            };

            _userRepositoryMock.Setup(x => x.GetByIdAsync(userId))
                .ReturnsAsync(new User
                {
                    Id = userId,
                    UserName = "TestUser",
                    Email = "testuser@example.com"
                });
            _favoriteRepositoryMock.Setup(x => x.GetAllByUser(userId)).ReturnsAsync(favorites);

            // Act
            var result = await _favoriteService.GetAllByUser(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());

            // Test for user not found
            _userRepositoryMock.Setup(x => x.GetByIdAsync(userId)).ReturnsAsync((User)null);
            var exception = await Assert.ThrowsAsync<Exception>(() => _favoriteService.GetAllByUser(userId));
            Assert.Equal("User does not exist.", exception.Message);
        }
    }
}