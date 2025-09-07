namespace Ordering.API.Endpoints
{
    public record DeleteOrderRequest(Guid Id);
    public record DeleteOrderResponse(bool IsSuccess);
    public class DeleteOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/orders/{Id}", async (Guid OrderId, ISender sender) =>
            {
                var command = new Application.Orders.Commands.DeleteOrder.DeleteOrderCommand(OrderId);
                var result = await sender.Send(command);
                var response = result.Adapt<DeleteOrderResponse>();
                return Results.Ok(response);
            })
            .WithName("DeleteOrder")
            .WithDescription("Delete Order")
            .WithSummary("Delete Order")
            .Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
}
