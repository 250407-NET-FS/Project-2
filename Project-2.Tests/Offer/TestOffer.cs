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

namespace Project_2.Tests
{
    public class OfferServiceTests
    {
        private readonly Mock<IOfferRepository> _offerRepositoryMock;
        private readonly Mock<IPropertyRepository> _propertyRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly OfferService _offerService;

        public OfferServiceTests()
        {
            _offerRepositoryMock = new Mock<IOfferRepository>();
            _propertyRepositoryMock = new Mock<IPropertyRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();

            _offerService = new OfferService(
                _offerRepositoryMock.Object,
                _propertyRepositoryMock.Object,
                _userRepositoryMock.Object
            );
        }

        // Test for AddAsync method with valid data
        [Fact]
        public async Task AddAsync_ShouldAddOffer_AndHandleErrors()
        {
            // Arrange valid data
            OfferNewDTO offerDto = new OfferNewDTO
            {
                PropertyId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                BidAmount = 100.0m
            };

            Property property = new Property("CountryName", "StateName", "CityName", "StreetName", "ZipCode", 1000.0m, 3, 500.0m)
            {
                PropertyID = offerDto.PropertyId
            };
            // Important: IdentityUser expects string ID, so use .ToString()
            User user = new User
            {
                Id = offerDto.UserId,
                UserName = "TestUser",
                Email = "testuser@example.com"
            };

            _propertyRepositoryMock.Setup(x => x.GetByIdAsync(offerDto.PropertyId)).ReturnsAsync(property);
            _userRepositoryMock.Setup(x => x.GetByIdAsync(offerDto.UserId)).ReturnsAsync(user);

            _offerRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Offer>())).Returns(Task.CompletedTask);
            _offerRepositoryMock.Setup(x => x.SaveChangesAsync()).Returns(Task.FromResult(1));

            // Act & Assert for valid data
            var result = await _offerService.AddAsync(offerDto);
            Assert.NotNull(result);
            Assert.Equal(offerDto.PropertyId, result.PropertyId);
            Assert.Equal(offerDto.UserId, result.UserId);
            Assert.Equal(offerDto.BidAmount, result.BidAmount);

            // Test for property not found
            _propertyRepositoryMock.Setup(x => x.GetByIdAsync(offerDto.PropertyId)).ReturnsAsync((Property)null);
            var exception = await Assert.ThrowsAsync<Exception>(() => _offerService.AddAsync(offerDto));
            Assert.Equal("Property cannot be null", exception.Message);

            // Test for user not found
            _propertyRepositoryMock.Setup(x => x.GetByIdAsync(offerDto.PropertyId)).ReturnsAsync(property);
            _userRepositoryMock.Setup(x => x.GetByIdAsync(offerDto.UserId)).ReturnsAsync((User)null);
            exception = await Assert.ThrowsAsync<Exception>(() => _offerService.AddAsync(offerDto));
            Assert.Equal("User cannot be null", exception.Message);

            // Test for invalid bid amount
            _userRepositoryMock.Setup(x => x.GetByIdAsync(offerDto.UserId)).ReturnsAsync(user);
            offerDto.BidAmount = -1.0m; // Invalid bid amount
            exception = await Assert.ThrowsAsync<Exception>(() => _offerService.AddAsync(offerDto));
            Assert.Equal("Bid amount must be greater than zero", exception.Message);
        }

        // Test for RemoveAsync method
        [Fact]
        public async Task RemoveAsync_ShouldRemoveOffer_AndHandleErrors()
        {
            // Arrange
            Guid offerId = Guid.NewGuid();
            Offer offer = new Offer(Guid.NewGuid(), Guid.NewGuid(), 100.0m) { OfferID = offerId };

            _offerRepositoryMock.Setup(x => x.GetByIdAsync(offerId)).ReturnsAsync(offer);
            _offerRepositoryMock.Setup(x => x.Remove(It.IsAny<Offer>()));

            // Act & Assert for valid data
            await _offerService.RemoveAsync(offerId);

            // Assert Remove method is called
            _offerRepositoryMock.Verify(x => x.Remove(It.IsAny<Offer>()), Times.Once);

            // Test for offer not found
            _offerRepositoryMock.Setup(x => x.GetByIdAsync(offerId)).ReturnsAsync((Offer)null);
            var exception = await Assert.ThrowsAsync<Exception>(() => _offerService.RemoveAsync(offerId));
            Assert.Equal("Offer not found", exception.Message);
        }

        // Test for GetByIdAsync method
        [Fact]
        public async Task GetByIdAsync_ShouldReturnOffer_AndHandleErrors()
        {
            // Arrange
            Guid offerId = Guid.NewGuid();
            Offer offer = new Offer(Guid.NewGuid(), Guid.NewGuid(), 100.0m) { OfferID = offerId };

            _offerRepositoryMock.Setup(x => x.GetByIdAsync(offerId)).ReturnsAsync(offer);

            // Act & Assert for valid data
            var result = await _offerService.GetByIdAsync(offerId);
            Assert.NotNull(result);
            Assert.Equal(offerId, result.OfferId);

            // Test for offer not found
            _offerRepositoryMock.Setup(x => x.GetByIdAsync(offerId)).ReturnsAsync((Offer)null);
            var exception = await Assert.ThrowsAsync<Exception>(() => _offerService.GetByIdAsync(offerId));
            Assert.Equal("Offer not found", exception.Message);
        }

        // Test for GetAllForProperty method
        [Fact]
        public async Task GetAllForProperty_ShouldReturnOffers_AndHandleErrors()
        {
            // Arrange
            Guid propertyId = Guid.NewGuid();
            List<Offer> offers = new List<Offer>
            {
                new Offer(Guid.NewGuid(), Guid.NewGuid(), 100.0m),
                new Offer(Guid.NewGuid(), Guid.NewGuid(), 150.0m)
            };

            _propertyRepositoryMock.Setup(x => x.GetByIdAsync(propertyId)).ReturnsAsync(new Property("CountryName", "StateName", "CityName", "StreetName", "ZipCode", 1000.0m, 3, 500.0m) { PropertyID = propertyId });
            _offerRepositoryMock.Setup(x => x.GetAllForProperty(propertyId)).ReturnsAsync(offers);

            // Act & Assert for valid data
            var result = await _offerService.GetAllForProperty(propertyId);
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());

            // Test for property not found
            _propertyRepositoryMock.Setup(x => x.GetByIdAsync(propertyId)).ReturnsAsync((Property)null);
            var exception = await Assert.ThrowsAsync<Exception>(() => _offerService.GetAllForProperty(propertyId));
            Assert.Equal("Property does not exist", exception.Message);
        }

        // Test for GetAllByUser method
        [Fact]
        public async Task GetAllByUser_ShouldReturnOffers_AndHandleErrors()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            List<Offer> offers = new List<Offer>
            {
                new Offer(Guid.NewGuid(), userId, 100.0m),
                new Offer(Guid.NewGuid(), userId, 150.0m)
            };

            _userRepositoryMock.Setup(x => x.GetByIdAsync(userId))
                .ReturnsAsync(new User
                {
                    Id = userId,
                    UserName = "TestUser",
                    Email = "testuser@example.com"
                });
            _offerRepositoryMock.Setup(x => x.GetAllByUser(userId)).ReturnsAsync(offers);

            // Act & Assert for valid data
            var result = await _offerService.GetAllByUser(userId);
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());

            // Test for user not found
            _userRepositoryMock.Setup(x => x.GetByIdAsync(userId)).ReturnsAsync((User)null);
            var exception = await Assert.ThrowsAsync<Exception>(() => _offerService.GetAllByUser(userId));
            Assert.Equal("User does not exist.", exception.Message);
        }

        // Test for SearchOffersAsync method
        [Fact]
        public async Task SearchOffersAsync_ShouldSearchOffers_AndHandleErrors()
        {
            // Arrange
            OfferSearchDTO searchDto = new OfferSearchDTO
            {
                OfferId = null,
                UserId = Guid.NewGuid(),
                PropertyId = Guid.NewGuid()
            };

            List<Offer> offers = new List<Offer>
            {
                new Offer(searchDto.UserId ?? Guid.Empty, searchDto.PropertyId ?? Guid.Empty, 100.0m),
                new Offer(searchDto.UserId ?? Guid.Empty, searchDto.PropertyId ?? Guid.Empty, 150.0m)
            };

            _offerRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(offers);

            // Act & Assert for valid search criteria
            var result = await _offerService.SearchOffersAsync(searchDto);
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());

            // Test for no search criteria (null)
            searchDto = null;
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _offerService.SearchOffersAsync(searchDto));
            Assert.Equal("At least one search criterion (OfferId, UserId, or PropertyId) must be provided", exception.Message);
        }
    }
}