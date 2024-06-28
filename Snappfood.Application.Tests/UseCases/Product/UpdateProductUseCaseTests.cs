using Moq;
using Snappfood.Application.Interfaces.Repositories;
using Snappfood.Application.Models.Request;
using Snappfood.Application.Exceptions;
using Snappfood.Application.Constants;
using Snappfood.Application.UseCases.Product;

namespace Snappfood.Application.Tests.UseCases.Product;

public class UpdateProductUseCaseTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly UpdateProductUseCase _updateProductUseCase;

    public UpdateProductUseCaseTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _updateProductUseCase = new UpdateProductUseCase(_productRepositoryMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldUpdateProduct_WhenValidRequest()
    {
        // Arrange
        var existingProduct = new Domain.Entities.Product { Id = 1, Title = "Product1", InventoryCount = 5, Price = 100, Discount = 10 };

        _productRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(existingProduct);

        var productUpdate = new ProductUpdate { Id = 1, InventoryCount = 10 };

        // Act
        await _updateProductUseCase.ExecuteAsync(productUpdate);

        // Assert
        _productRepositoryMock.Verify(x => x.UpdateAsync(It.Is<Domain.Entities.Product>(p => p.InventoryCount == 10)), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrowNotFoundException_WhenProductNotFound()
    {
        // Arrange
        var productUpdate = new ProductUpdate { Id = 1, InventoryCount = 10 };
        _productRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync((Domain.Entities.Product)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<NotFoundException>(() => _updateProductUseCase.ExecuteAsync(productUpdate));
        Assert.Equal(ExceptionMessages.ProductNotFound, exception.Message);
    }
}