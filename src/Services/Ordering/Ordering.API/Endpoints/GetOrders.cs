using BuildingBlocks.Pagination;
using Ordering.Application.Orders.Queries.GetOrders;

namespace Ordering.API.Endpoints
{
    public record GetOrderResponse(PaginationResult<OrderDto> Orders);
    public class GetOrders : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders", async ([AsParameters] PaginationRequest request, ISender sender) =>
            {
                var query = new GetOrdersQuery(request);
                var orders = await sender.Send(query);
                var ordersDto = orders.Adapt<GetOrderResponse>();
                return Results.Ok(ordersDto);
            })
            .WithName("GetOrders")
            .WithDescription("Get Orders")
            .WithSummary("Get Orders")
            .Produces<GetOrderResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
}
