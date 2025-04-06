
namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdQuery(Guid id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product product);
    public class GetProductByIdQueryHandler(IDocumentSession session,ILogger<GetProductByIdQueryHandler> logger) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("Executing GetProductByIdQueryHandler.Handle method for Query {0}", query);
            var result = await session.LoadAsync<Product>(query.id,cancellationToken);
            if (result == null)
            {
                throw new ProductNotFoundException();
            }
            return new GetProductByIdResult(result);
        }
    }
}
