using Moq;
using Snappfood.Application.Interfaces.Repositories;
using Snappfood.Application.Models.Request;
using Snappfood.Application.UseCases.Order;
using Snappfood.Domain.Entities;
using Snappfood.Application.Exceptions;
using Snappfood.Application.Constants;

namespace Snappfood.Application.Tests.UseCases.Order;


public class AddOrderUseCaseTests
{
    private readonly Mock<IOrderRepository> _orderRepositoryMock;
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly AddOrderUseCase _addOrderUseCase;

    public AddOrderUseCaseTests()
    {
        _orderRepositoryMock = new Mock<IOrderRepository>();
        _productRepositoryMock = new Mock<IProductRepository>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _addOrderUseCase = new AddOrderUseCase(_orderRepositoryMock.Object, _productRepositoryMock.Object, _userRepositoryMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldAddOrder_WhenValidRequest()
    {
        // Arrange
        var product = new Domain.Entities.Product { Id = 1, InventoryCount = 5, Price = 100, Discount = 10, Title = "Product1" };
        var user = new User { Id = 1, Name = "User1" };

        _productRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(product);
        _userRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(user);

        var orderCreate = new OrderCreate { ProductId = 1, BuyerId = 1 };

        // Act
        await _addOrderUseCase.ExecuteAsync(orderCreate);

        // Assert
        _orderRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Domain.Entities.Order>()), Times.Once);
        _productRepositoryMock.Verify(x => x.UpdateAsync(It.Is<Domain.Entities.Product>(p => p.InventoryCount == 4)), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrowNotFoundException_WhenProductNotFound()
    {
        // Arrange
        _productRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync((Domain.Entities.Product)null);
        var orderCreate = new OrderCreate { ProductId = 1, BuyerId = 1 };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<NotFoundException>(() => _addOrderUseCase.ExecuteAsync(orderCreate));
        Assert.Equal(ExceptionMessages.ProductNotFound, exception.Message);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrowNotFoundException_WhenUserNotFound()
    {
        // Arrange
        var product = new Domain.Entities.Product { Id = 1, InventoryCount = 5, Price = 100, Discount = 10, Title = "Product1" };
        _productRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(product);
        _userRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync((User)null);

        var orderCreate = new OrderCreate { ProductId = 1, BuyerId = 1 };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<NotFoundException>(() => _addOrderUseCase.ExecuteAsync(orderCreate));
        Assert.Equal(ExceptionMessages.UserNotFound, exception.Message);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrowValidationException_WhenInventoryCountIsZero()
    {
        // Arrange
        var product = new Domain.Entities.Product { Id = 1, InventoryCount = 0, Price = 100, Discount = 10, Title = "Product1" };
        var user = new User { Id = 1, Name = "User1" };

        _productRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(product);
        _userRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(user);

        var orderCreate = new OrderCreate { ProductId = 1, BuyerId = 1 };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => _addOrderUseCase.ExecuteAsync(orderCreate));
        Assert.Equal(ExceptionMessages.InventoryCountShouldMoreThan0, exception.Message);
    }
}