
using Basket.API.Data;

namespace Basket.API.Basket.GetBasket
{
    public record GetBasketQuery(string userName) : IQuery<GetBasketResult>;
    public record GetBasketResult(ShoppingCart shoppingCart);
    public class GetBasketQueryHandler(IBasketRepository _repository) : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            var basket = await _repository.GetBasket(request.userName,cancellationToken);

            return new GetBasketResult(basket);
        }
    }
}
