using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;
using Project_2.Models;
using Project_2.Services.Services;
using Project_2.Data;
using Project_2.Models.DTOs;
using Moq;
using Microsoft.EntityFrameworkCore;

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
            var options = new DbContextOptionsBuilder<JazaContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new JazaContext(options);
            _propertyRepositoryMock = new Mock<IPropertyRepository>();
            _purchaseRepositoryMock = new Mock<IPurchaseRepository>();
            _purchaseService = new PurchaseService(_purchaseRepositoryMock.Object, new Mock<IUnitOfWork>().Object);
            _propertyService = new PropertyService(_propertyRepositoryMock.Object, context);
        }

        [Fact]
        public void Dispose()
        {
            var context = _propertyService.GetType()
                .GetField("_context", BindingFlags.NonPublic | BindingFlags.Instance)?
                .GetValue(_propertyService) as JazaContext;

            context?.Database.EnsureDeleted();
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
                PropertyID = propertyId,
                OwnerID = userId
            };

            _propertyRepositoryMock
                .Setup(repo => repo.GetByIdAsync(propertyId))
                .ReturnsAsync(property);
            _propertyRepositoryMock
                .Setup(repo => repo.SaveChangesAsync())
                .ReturnsAsync(1);


            await _propertyService.RemoveProperty(propertyId, userId);

            _propertyRepositoryMock.Verify(repo => repo.Remove(It.Is<Property>(p =>
                p.PropertyID == propertyId &&
                p.OwnerID == userId)), Times.Once);
            _propertyRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task MarkForSaleAsync_ShouldUpdatePropertyCorrectly()
        {
            var propertyId = Guid.NewGuid();
            var ownerId = Guid.NewGuid();
            var property = new Property(
                "USA", "NY", "NYC", "10001",
                "123 Test St", 500000, 3, 2)
            {
                PropertyID = propertyId,
                OwnerID = ownerId,
                ForSale = false
            };

            _propertyRepositoryMock
                .Setup(repo => repo.GetByIdAsync(propertyId))
                .ReturnsAsync(property);
            _propertyRepositoryMock
                .Setup(repo => repo.SaveChangesAsync())
                .ReturnsAsync(1);

            await _propertyService.MarkForSaleAsync(propertyId);

            _propertyRepositoryMock.Verify(repo => repo.Update(It.Is<Property>(p =>
                p.PropertyID == propertyId &&
                p.ForSale == true)));
        }

        [Fact]
        public async Task AddNewPropertyAsync_ShouldAddProperty()
        {
            var property = new Property(
                "USA", "NY", "NYC", "10001",
                "123 Test St", 500000, 3, 2);

            _propertyRepositoryMock
                .Setup(repo => repo.SaveChangesAsync())
                .ReturnsAsync(1);

            // Act
            await _propertyService.AddNewPropertyAsync(property);

            // Assert
            _propertyRepositoryMock.Verify(repo => repo.AddAsync(property));
            _propertyRepositoryMock.Verify(repo => repo.SaveChangesAsync());
        }

        [Fact]
        public async Task MarkSoldAsync_WhenPropertyNotFound_ShouldThrowException()
        {

            var propertyId = Guid.NewGuid();
            var newOwnerId = Guid.NewGuid();

            _propertyRepositoryMock
                .Setup(repo => repo.GetByIdAsync(propertyId))
                .ReturnsAsync((Property?)null);


            var exception = await Assert.ThrowsAsync<Exception>(
                () => _propertyService.MarkSoldAsync(propertyId, newOwnerId));
            Assert.Equal("Property not found", exception.Message);
        }

        [Fact]
        public async Task RemoveProperty_WhenUnauthorized_ShouldThrowException()
        {

            var propertyId = Guid.NewGuid();
            var ownerId = Guid.NewGuid();
            var unauthorizedUserId = Guid.NewGuid();
            var property = new Property(
                "USA", "NY", "NYC", "10001",
                "123 Test St", 500000, 3, 2)
            {
                PropertyID = propertyId,
                OwnerID = ownerId
            };

            _propertyRepositoryMock
                .Setup(repo => repo.GetByIdAsync(propertyId))
                .ReturnsAsync(property);

            var exception = await Assert.ThrowsAsync<Exception>(
                () => _propertyService.RemoveProperty(propertyId, unauthorizedUserId));
            Assert.Equal("Unauthorized", exception.Message);
        }

        [Fact]
        public async Task AddNewPropertyAsync_WhenSaveFails_ShouldThrowException()
        {

            var property = new Property(
                "USA", "NY", "NYC", "10001",
                "123 Test St", 500000, 3, 2);

            _propertyRepositoryMock
                .Setup(repo => repo.SaveChangesAsync())
                .ReturnsAsync(0);


            var exception = await Assert.ThrowsAsync<Exception>(
                () => _propertyService.AddNewPropertyAsync(property));
            Assert.Equal("Failed to insert property", exception.Message);
        }
        [Fact]
        public async Task MarkSoldAsync_WhenSaveFails_ShouldThrowException()
        {
            var propertyId = Guid.NewGuid();
            var newOwnerId = Guid.NewGuid();
            var property = new Property(
                "USA", "NY", "NYC", "10001",
                "123 Test St", 500000, 3, 2)
            {
                PropertyID = propertyId
            };

            _propertyRepositoryMock
                .Setup(repo => repo.GetByIdAsync(propertyId))
                .ReturnsAsync(property);
            _propertyRepositoryMock
                .Setup(repo => repo.SaveChangesAsync())
                .ReturnsAsync(0);


            var exception = await Assert.ThrowsAsync<Exception>(
                () => _propertyService.MarkSoldAsync(propertyId, newOwnerId));
            Assert.Equal("Failed to update property", exception.Message);
        }
        [Fact]
        public async Task GetByIdAsync_ShouldReturnProperty()
        {

            var propertyId = Guid.NewGuid();
            var expected = new Property(
                "USA", "NY", "NYC", "10001",
                "123 Test St", 500000, 3, 2)
            {
                PropertyID = propertyId
            };

            _propertyRepositoryMock
                .Setup(repo => repo.GetByIdAsync(propertyId))
                .ReturnsAsync(expected);


            var result = await _propertyService.GetByIdAsync(propertyId);

            Assert.NotNull(result);
            Assert.Equal(propertyId, result.PropertyID);
            _propertyRepositoryMock.Verify(repo => repo.GetByIdAsync(propertyId));
        }

        [Fact]
        public async Task GetByIdAsync_WhenNotFound_ShouldReturnNull()
        {
            var propertyId = Guid.NewGuid();
            _propertyRepositoryMock
                .Setup(repo => repo.GetByIdAsync(propertyId))
                .ReturnsAsync((Property?)null);


            var result = await _propertyService.GetByIdAsync(propertyId);


            Assert.Null(result);
            _propertyRepositoryMock.Verify(repo => repo.GetByIdAsync(propertyId));
        }

        [Fact]
        public async Task MarkForSaleAsync_WhenSaveFails_ShouldThrowException()
        {
            var propertyId = Guid.NewGuid();
            var property = new Property(
                "USA", "NY", "NYC", "10001",
                "123 Test St", 500000, 3, 2)
            {
                PropertyID = propertyId,
                ForSale = false
            };

            _propertyRepositoryMock
                .Setup(repo => repo.GetByIdAsync(propertyId))
                .ReturnsAsync(property);
            _propertyRepositoryMock
                .Setup(repo => repo.SaveChangesAsync())
                .ReturnsAsync(0);

            var exception = await Assert.ThrowsAsync<Exception>(
                () => _propertyService.MarkForSaleAsync(propertyId));
            Assert.Equal("Failed to update property", exception.Message);
        }

        [Fact]
        public async Task MarkForSaleAsync_WhenPropertyNotFound_ShouldThrowException()
        {
            var propertyId = Guid.NewGuid();

            _propertyRepositoryMock
                .Setup(repo => repo.GetByIdAsync(propertyId))
                .ReturnsAsync((Property?)null);

            var exception = await Assert.ThrowsAsync<Exception>(
                () => _propertyService.MarkForSaleAsync(propertyId));
            Assert.Equal("Property not found", exception.Message);
        }
        [Fact]
        public async Task MarkSoldAsync_ShouldUpdatePropertyCorrectly()
        {
            var propertyId = Guid.NewGuid();
            var oldOwnerId = Guid.NewGuid();
            var newOwnerId = Guid.NewGuid();
            var property = new Property(
                "USA", "NY", "NYC", "10001",
                "123 Test St", 500000, 3, 2)
            {
                PropertyID = propertyId,
                OwnerID = oldOwnerId,
                ForSale = true
            };

            _propertyRepositoryMock
                .Setup(repo => repo.GetByIdAsync(propertyId))
                .ReturnsAsync(property);
            _propertyRepositoryMock
                .Setup(repo => repo.SaveChangesAsync())
                .ReturnsAsync(1);


            await _propertyService.MarkSoldAsync(propertyId, newOwnerId);

            _propertyRepositoryMock.Verify(repo => repo.Update(It.Is<Property>(p =>
                p.PropertyID == propertyId &&
                p.OwnerID == newOwnerId &&
                p.ForSale == false)));
        }
    }
}