namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketRequest(ShoppingCart shoppingCart);
    public record StoreBasketResponse(string userName);
    public class StoreBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket", async (StoreBasketRequest request, ISender sender) =>
            {
                var command = request.Adapt<StoreBasketCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<StoreBasketResponse>();
                return Results.Created($"/basket/{response.userName}",response);
            })
            .WithName("StoreBasket")
            .Produces(StatusCodes.Status201Created, typeof(StoreBasketResponse))
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Store Basket")
            .WithDescription("Store Basket");
        }
    }
}
