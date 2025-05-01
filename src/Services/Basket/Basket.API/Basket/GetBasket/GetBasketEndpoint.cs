namespace Basket.API.Basket.GetBasket
{
    public record GetBasketRequest(string userName);
    public record GetBasketResponse(ShoppingCart shoppingCart);
    public class GetBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
            {
                var basketRequest = new GetBasketRequest(userName);
                var query = basketRequest.Adapt<GetBasketQuery>();
                var result = await sender.Send(query);
                var response = result.Adapt<GetBasketResponse>();
                return Results.Ok(response);
            })
            .WithName("GetBasket")
            .Produces(StatusCodes.Status200OK,typeof(GetBasketResponse))
            .WithSummary("Get Basket By UserName")
            .WithDescription("Get Basket By UserName");
        }
    }
}
