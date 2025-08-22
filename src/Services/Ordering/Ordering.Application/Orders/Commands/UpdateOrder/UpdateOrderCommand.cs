using FluentValidation;

namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public record UpdateOrderCommand(OrderDto OrderDto) : ICommand<UpdateOrderResult>;
    public record UpdateOrderResult(bool IsSuccess);

    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(c => c.OrderDto.OrderName).NotEmpty().WithMessage("Order name is required.");
            RuleFor(c => c.OrderDto.CustomerId).NotNull().WithMessage("Customer ID is required.");
            RuleFor(c => c.OrderDto.OrderItems).NotEmpty().WithMessage("Order items cannot be empty.");
        }
    }
}
