using Xunit;
using Project_2.Models;
using Project_2.Services.Services;
using Project_2.Data;
using Project_2.Models.DTOs;
using Moq;

//only tests valid atm

namespace Project_2.Tests;

public class TestPurchase
{
    // Moq repository
    private readonly Mock<IPurchaseRepository> _purchaseRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly PurchaseService _purchaseService;

    public TestPurchase()
    {
        _purchaseRepositoryMock = new Mock<IPurchaseRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        // configure unit of work using setup
        _unitOfWorkMock.Setup(uow => uow.PurchaseRepo).Returns(_purchaseRepositoryMock.Object);
        _unitOfWorkMock.Setup(uow => uow.PropertyRepo).Returns(new Mock<IPropertyRepository>().Object);
        _unitOfWorkMock.Setup(uow => uow.OfferRepo).Returns(new Mock<IOfferRepository>().Object);

        _purchaseService = new PurchaseService(
            _purchaseRepositoryMock.Object,
            _unitOfWorkMock.Object
        );
    }

    [Fact] //test get all purchases
    public async Task GetAllAsync_ShouldReturnAllPurchases()
    {
        var expectedPurchases = new List<Purchase>
        {
            // Create a list of Purchase objects with unique IDs and amounts.
            new Purchase(Guid.NewGuid(), Guid.NewGuid(), 100000),
            new Purchase(Guid.NewGuid(), Guid.NewGuid(), 200000)
        };
        // Set up the mock repository to return the expected purchases when GetAllAsync is called.
        _purchaseRepositoryMock
            .Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(expectedPurchases);


        var result = await _purchaseService.GetAllAsync();

        Assert.NotNull(result); //not null
        Assert.Equal(expectedPurchases.Count, result.Count()); //correct count of purchases
        Assert.Equal(expectedPurchases, result); //correct purchases returned
    }

    [Fact] //test get purchase by id
    public async Task GetByIdAsync_ShouldReturnPurchaseById()
    {
        var purchaseId = Guid.NewGuid();
        var propertyId = Guid.NewGuid();
        var expectedPurchase = new Purchase(purchaseId, propertyId, 100000);

        _purchaseRepositoryMock
            .Setup(repo => repo.GetByIdAsync(purchaseId))
            .ReturnsAsync(expectedPurchase);

        var result = await _purchaseService.GetByIdAsync(purchaseId);

        Assert.NotNull(result); //not null
        Assert.Equal(expectedPurchase.PurchaseID, result.PurchaseID); //correct purchase ID
        _purchaseRepositoryMock.Verify(repo => repo.GetByIdAsync(purchaseId), Times.Once);
    }

    [Fact] //test purchase creation valid
    public async Task CreateAsync_ShouldCreatePurchase()
    {
        var propertyId = Guid.NewGuid();
        var buyerId = Guid.NewGuid();
        decimal bidAmount = 2500000000000;
        var offerId = Guid.NewGuid();
        var ownerId = Guid.NewGuid();

        //moq a property
        //string Country, string State, string City, string ZipCode, string StreetAddress, 
        // decimal StartingPrice, int Bedrooms, decimal Bathrooms
        var property = new Property(
            "usa",
            "colorado",
            "tesla",
            "1234",
            "test street",
            100000,
            1,
            1
        )
        {
            PropertyID = propertyId,
            OwnerID = ownerId,
            ForSale = true
        };

        //moq a offer
        var offer = new Offer(
            buyerId,
            propertyId,
            bidAmount
        );

        var purchaseDTO = new CreatePurchaseDTO
        {
            PropertyId = propertyId,
            UserId = ownerId,
            OfferId = offerId,
        };

        //set up property moq
        var propertyRepositoryMock = new Mock<IPropertyRepository>();
        _unitOfWorkMock.Setup(uow => uow.PropertyRepo).Returns(propertyRepositoryMock.Object);
        propertyRepositoryMock.Setup(repo => repo.GetByIdAsync(propertyId)).ReturnsAsync(property);

        //set up offer moq
        var offerRepoMock = new Mock<IOfferRepository>();
        _unitOfWorkMock.Setup(uow => uow.OfferRepo).Returns(offerRepoMock.Object);
        offerRepoMock.Setup(repo => repo.GetByIdAsync(offerId)).ReturnsAsync(offer);

        await _purchaseService.AcceptOffer(purchaseDTO);

        // Verify that the purchase was created with the correct parameters
        _unitOfWorkMock.Verify(uow => uow.PurchaseRepo.AddAsync(
            It.Is<Purchase>(p =>
            p.OwnerID == buyerId &&      //// Check that the OwnerID of the Purchase matches the buyerId.
            p.PropertyID == propertyId &&
            p.FinalPrice == bidAmount))
            );

        //check removeallforproperty runs
        offerRepoMock.Verify(repo => repo.RemoveAllForProperty(propertyId));
        _unitOfWorkMock.Verify(uow => uow.CommitAsync());
    }
}
