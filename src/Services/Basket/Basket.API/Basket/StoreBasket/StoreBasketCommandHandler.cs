using Basket.API.Data;
using DiscountGrpc.Grpc;

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
    public class StoreBasketCommandHandler(IBasketRepository _repository, DiscountProtoService.DiscountProtoServiceClient discountProto) 
        : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            //Calculate Discount for each Item
            await DeductDiscount(command, cancellationToken);

            //To Do : Store basket Update or Insert in DB
            //Update cache
            var basket = await _repository.StoreBasket(command.shoppingCart, cancellationToken);

            return new StoreBasketResult(basket.UserName);
        }

        private async Task DeductDiscount(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            foreach (var item in command.shoppingCart.Items)
            {
                var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest() { ProductName = item.ProductName }, cancellationToken: cancellationToken);
                item.Price -= coupon.Amount;
            }
        }
    }
}
