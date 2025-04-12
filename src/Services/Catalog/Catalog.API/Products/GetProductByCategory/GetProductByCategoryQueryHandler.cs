
namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductByCategoryQuery(string category) : IQuery<GetProductByCategoryResult>;
    public record GetProductByCategoryResult(IEnumerable<Product> products);
    public class GetProductByCategoryQueryHandler(IDocumentSession session,ILogger<GetProductByCategoryQueryHandler> logger) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("Executing GetProductByCategoryQueryHandler.Handle method for Query {0}", query);
            var result = await session.Query<Product>().Where(p => p.Category.Contains(query.category)).ToListAsync(cancellationToken);
            return new GetProductByCategoryResult(result);
        }
    }
}
