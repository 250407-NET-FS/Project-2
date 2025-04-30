using Project_2.Models;
using Project_2.Services.Services;
using Project_2.Data;
using Project_2.Models.DTOs;
using Moq;

namespace Project_2.Tests
{
    public class TestProperty
    {
        private readonly Mock<IPurchaseRepository> _purchaseRepositoryMock;
        private readonly PurchaseService _purchaseService;
        private readonly Mock<IPropertyRepository> _propertyRepositoryMock;
        private readonly PropertyService _propertyService;

        public TestProperty()
        {
            _purchaseRepositoryMock = new Mock<IPurchaseRepository>();
            _propertyRepositoryMock = new Mock<IPropertyRepository>();
            _purchaseService = new PurchaseService(_purchaseRepositoryMock.Object, new Mock<IUnitOfWork>().Object);
            _propertyService = new PropertyService(_propertyRepositoryMock.Object, new Mock<JazaContext>().Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllProperties()
        {
            var expectedProperties = new List<Property>
            { //Property.Property(string Country, string State, string City, 
            // string ZipCode, string StreetAddress, decimal StartingPrice, int Bedrooms, decimal Bathrooms)
                 new Property(
                    "USA",
                    "florida",
                    "orlando",
                    "55555",
                    "123 florida man st",
                    1,
                    100,
                    20
                    ),
                    new Property(
                    "idk",
                    "moon",
                    "blackhole",
                    "787878",
                    "123 oblivion",
                    111,
                    1,
                    1
                    ),
                    new Property(
                    "CANADA",
                    "Ontario",
                    "quebec",
                    "88889",
                    "123 maple street",
                    9999999999,
                    100000,
                    200000
                    )
            };
            var propertyRepositoryMock = new Mock<IPropertyRepository>();
            propertyRepositoryMock
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(expectedProperties);
            var result = await propertyRepositoryMock.Object.GetAllAsync();
            Assert.NotNull(result);
            propertyRepositoryMock.Verify(repo => repo.GetAllAsync());
        }

        [Fact]
        public async Task GetAllFilterAsync_ShouldReturnFilteredProperties()
        {
            var expectedProperties = new List<Property>
            { //Property.Property(string Country, string State, string City, 
            // string ZipCode, string StreetAddress, decimal StartingPrice, int Bedrooms, decimal Bathrooms)
                new Property(
                    "USA",
                    "florida",
                    "orlando",
                    "55555",
                    "123 florida man st",
                    1,
                    100,
                    20
                    ),
                new Property(
                    "CANADA",
                    "Ontario",
                    "quebec",
                    "88889",
                    "123 maple street",
                    9999999999,
                    100000,
                    200000
                    )
            };

            _propertyRepositoryMock
                .Setup(repo => repo.GetAllWithFilters(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<decimal>(), It.IsAny<int>(), It.IsAny<decimal>()))
                .ReturnsAsync(expectedProperties);

            // Act
            var result = await _propertyService.ShowAvailablePropertiesAsync("USA", "NY", "10001", "123 Main St", 400000, 600000, 3, 2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedProperties.Count, result.Count());
        }

        [Fact]
        public async Task GetAllAsync_WhenNoProperties_ShouldReturnEmptyList()
        {
            var propertyRepositoryMock = new Mock<IPropertyRepository>();
            propertyRepositoryMock
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<Property>());

            var result = await propertyRepositoryMock.Object.GetAllAsync();

            Assert.Empty(result);
            propertyRepositoryMock.Verify(repo => repo.GetAllAsync());
        }

        [Fact]
        public async Task GetAllAsync_WhenRepositoryThrows_ShouldPropagateException()
        {
            var propertyRepositoryMock = new Mock<IPropertyRepository>();
            propertyRepositoryMock
                .Setup(repo => repo.GetAllAsync())
                .ThrowsAsync(new Exception("Database error"));

            await Assert.ThrowsAsync<Exception>(() =>
                propertyRepositoryMock.Object.GetAllAsync());
        }

        [Fact]
        public async Task GetAllAsync_WithInvalidProperty_ShouldHandleValidation()
        {
            var invalidProperty = new Property(
                "",  // Empty country
                "",  // Empty state
                "city",
                "12345",
                "address",
                -1,  // Negative price
                -2,  // Negative bedrooms
                -3   // Negative bathrooms
            );

            var propertyRepositoryMock = new Mock<IPropertyRepository>();
            propertyRepositoryMock
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<Property> { invalidProperty });

            var result = await propertyRepositoryMock.Object.GetAllAsync();
            Assert.NotNull(result);
            var property = result.First();
            Assert.True(string.IsNullOrEmpty(property.Country));
            Assert.True(string.IsNullOrEmpty(property.State));
            Assert.Equal(-1, property.StartingPrice);
        }

        [Fact]
        public async Task RemoveAsync_ShouldRemoveProperty()
        {
            // Arrange
            var propertyId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var property = new Property(
                "CANADA",
                "Ontario",
                "quebec",
                "88889",
                "123 maple street",
                9999999999,
                100000,
                200000
            )
            {

                OwnerID = userId
            };

            _propertyRepositoryMock
                .Setup(repo => repo.GetByIdAsync(propertyId))
                .ReturnsAsync(property);
            _propertyRepositoryMock
                .Setup(repo => repo.SaveChangesAsync())
                .ReturnsAsync(1);

            await _propertyService.RemoveProperty(propertyId, userId);

            _propertyRepositoryMock.Verify(repo => repo.Remove(property), Times.Once);
            _propertyRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }
    }
}