using BuildingBlocks.CQRS;
using FluentValidation;
using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand(OrderDto Order): ICommand<CreateOrderResult>;

    public record CreateOrderResult(Guid Id);

    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(c => c.Order.OrderName).NotEmpty().WithMessage("Order name is required.");
            RuleFor(c => c.Order.CustomerId).NotNull().WithMessage("Customer ID is required.");
            RuleFor(c => c.Order.OrderItems).NotEmpty().WithMessage("Order items cannot be empty.");
        }
    }
}
