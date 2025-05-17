using Basket.API.Data;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart shoppingCart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(string userName);

    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.shoppingCart).NotNull().WithMessage("Shopping cart cannot be null");
            RuleFor(x => x.shoppingCart.UserName).NotEmpty().WithMessage("Username cannot be Empty");

        }
    }
    public class StoreBasketCommandHandler(IBasketRepository _repository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            //To Do : Store basket Update or Insert in DB
            //Update cache
            var basket = await _repository.StoreBasket(command.shoppingCart, cancellationToken);

            return new StoreBasketResult(basket.UserName);
        }
    }
}
