
namespace Basket.API.Basket.GetBasket
{
    public record GetBasketQuery(string userName) : IQuery<GetBasketResult>;
    public record GetBasketResult(ShoppingCart shoppingCart);
    public class GetBasketQueryHandler : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            //To DO: get basket from database
            //var basket = await _repository.GetBasket(userName);

            return new GetBasketResult(new ShoppingCart("swn"));
        }
    }
}
