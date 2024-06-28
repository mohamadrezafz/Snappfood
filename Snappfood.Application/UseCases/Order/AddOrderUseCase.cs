using Snappfood.Application.Constants;
using Snappfood.Application.Exceptions;
using Snappfood.Application.Interfaces.Repositories;
using Snappfood.Application.Models.Request;

namespace Snappfood.Application.UseCases.Order;
public class AddOrderUseCase
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUserRepository _userRepository;

    public AddOrderUseCase(IOrderRepository orderRepository, IProductRepository productRepository, IUserRepository userRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _userRepository = userRepository;
    }

    public async Task ExecuteAsync(OrderCreate order)
    {
        var product = await _productRepository.GetByIdAsync(order.ProductId);
        if (product == null)
            throw new NotFoundException(ExceptionMessages.ProductNotFound);

        var user = await _userRepository.GetByIdAsync(order.BuyerId);
        if (user == null)
            throw new NotFoundException(ExceptionMessages.UserNotFound);

        if (product.InventoryCount == 0)
            throw new Exception(ExceptionMessages.InventoryCountShouldMoreThan0);

        var entity = new Domain.Entities.Order(order.ProductId, order.BuyerId);
        await _orderRepository.AddAsync(entity);

        product.InventoryCount -= 1;
        await _productRepository.UpdateAsync(product);
    }
}