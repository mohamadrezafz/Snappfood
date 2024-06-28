using Moq;
using Snappfood.Application.Interfaces.Repositories;
using Snappfood.Application.Models.Request;
using Snappfood.Application.Constants;
using Snappfood.Application.UseCases.Product;
namespace Snappfood.Application.Tests.UseCases.Product;

public class AddProductUseCaseTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly AddProductUseCase _addProductUseCase;

    public AddProductUseCaseTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _addProductUseCase = new AddProductUseCase(_productRepositoryMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldAddProduct_WhenValidRequest()
    {
        // Arrange
        var productCreate = new ProductCreate
        {
            Title = "New Product",
            InventoryCount = 10,
            Price = 100,
            Discount = 10
        };

        _productRepositoryMock.Setup(x => x.GetByTitleAsync("New Product")).ReturnsAsync((Domain.Entities.Product)null);

        // Act
        await _addProductUseCase.ExecuteAsync(productCreate);

        // Assert
        _productRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Domain.Entities.Product>()), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrowValidationException_WhenProductTitleIsDuplicate()
    {
        // Arrange
        var productCreate = new ProductCreate
        {
            Title = "Duplicate Product",
            InventoryCount = 10,
            Price = 100,
            Discount = 10
        };

        var existingProduct = new Domain.Entities.Product { Id = 1, Title = "Duplicate Product", InventoryCount = 5, Price = 50, Discount = 5 };

        _productRepositoryMock.Setup(x => x.GetByTitleAsync("Duplicate Product")).ReturnsAsync(existingProduct);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => _addProductUseCase.ExecuteAsync(productCreate));
        Assert.Equal(ExceptionMessages.ProductTitleIsDuplicate, exception.Message);
    }
}
