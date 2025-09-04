
using BuildingBlocks.Pagination;

namespace Ordering.Application.Orders.Queries.GetOrders
{
    public class GetOrdersQueryHandler(IApplicationDbContext context)
        : IQueryHandler<GetOrdersQuery, GetOrdersResult>
    {
        public async Task<GetOrdersResult> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var pageIndex = request.Pagination.pageIndex;
            var pageSize = request.Pagination.pageSize;
            var query = context.Orders.Include(x => x.OrderItems).AsNoTracking();
            var totalCount = await query.LongCountAsync(cancellationToken);
            var orders = await query.OrderBy(o => o.OrderName.Value)
                                  .Skip(pageIndex * pageSize)
                                  .Take(pageSize)
                                  .ToListAsync(cancellationToken);
            return new GetOrdersResult(new PaginationResult<OrderDto>(pageIndex, pageSize, totalCount, orders.ToOrderDtoList()));
        }
    }
}
