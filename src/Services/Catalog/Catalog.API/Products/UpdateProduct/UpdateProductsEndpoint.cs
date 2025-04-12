
namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductRequest(string name, List<string> category, string description, string imageFile, decimal price);
    public record UpdateProductResponse(bool isSuccess);
    public class UpdateProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products/{id}", async (Guid id, UpdateProductRequest request, ISender sender) =>
            {
                var command = new UpdateProductsCommand(id, request.name, request.category, request.description, request.imageFile, request.price);
                var result = await sender.Send(command);
                var response = result.Adapt<UpdateProductResponse>();
                return Results.Ok(response);
            })
            .WithName("UpdateProduct")
            .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithDescription("Update Product")
            .WithSummary("Update Product");
        }
    }
}
