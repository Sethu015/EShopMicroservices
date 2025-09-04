namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer
{
    public class GetOrdersByCustomerQueryHandler(IApplicationDbContext context)
        : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
    {
        public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery request, CancellationToken cancellationToken)
        {
            var orders = await context.Orders.Include(x => x.OrderItems)
                         .AsNoTracking()
                         .Where(o => o.CustomerId.Value == request.CustomerId)
                         .OrderBy(o => o.OrderName.Value)
                         .ToListAsync(cancellationToken);
            if(orders is null)
                throw new OrderNotFoundException(request.CustomerId);
            return new GetOrdersByCustomerResult(orders.ToOrderDtoList());
        }
    }
}
