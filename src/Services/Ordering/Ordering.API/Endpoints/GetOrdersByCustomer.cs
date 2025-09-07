using Ordering.Application.Orders.Queries.GetOrdersByCustomer;

namespace Ordering.API.Endpoints
{
    public record GetOrdersByCustomerRequest(Guid CustomerId);
    public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> Orders);
    public class GetOrdersByCustomer : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/customer/{CustomerId}", async (Guid CustomerId, ISender sender) =>
            {
                var orders = await sender.Send(new GetOrdersByCustomerQuery(CustomerId));
                var ordersDto = orders.Adapt<GetOrdersByCustomerResponse>();
                return Results.Ok(ordersDto);
            })
            .WithName("GetOrdersByCustomer")
            .WithDescription("Get Orders by Customer Id")
            .WithSummary("Get Orders by Customer Id")
            .Produces<GetOrdersByCustomerResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
}
