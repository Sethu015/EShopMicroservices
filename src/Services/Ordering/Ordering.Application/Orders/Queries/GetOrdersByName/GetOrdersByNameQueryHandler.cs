namespace Ordering.Application.Orders.Queries.GetOrdersByName
{
    public class GetOrdersByNameQueryHandler(IApplicationDbContext context)
        : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
    {
        public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery request, CancellationToken cancellationToken)
        {
            var orders = await context.Orders.Include(x => x.OrderItems)
                         .AsNoTracking()
                         .Where(o => o.OrderName.Value.Contains(request.OrderName,StringComparison.OrdinalIgnoreCase))
                         .OrderBy(o => o.OrderName)
                         .ToListAsync(cancellationToken);
            var orderDtos = ProjectToOrderDto(orders);
            return new GetOrdersByNameResult(orderDtos);
        }

        private List<OrderDto> ProjectToOrderDto(List<Order> orders)
        {
            var orderDtos = new List<OrderDto>();
            foreach (var order in orders)
            {
                var orderDto = new OrderDto(order.Id.Value
                    , order.CustomerId.Value,
                    order.OrderName.Value,
                    new AddressDto(order.ShippingAddress.FirstName,
                    order.ShippingAddress.LastName,
                    order.ShippingAddress.EmailAddress,
                    order.ShippingAddress.AddressLine,
                    order.ShippingAddress.Country,
                    order.ShippingAddress.State,
                    order.ShippingAddress.Zip),
                    new AddressDto(order.BillingAddress.FirstName,
                    order.BillingAddress.LastName,
                    order.BillingAddress.EmailAddress,
                    order.BillingAddress.AddressLine,
                    order.BillingAddress.Country,
                    order.BillingAddress.State,
                    order.BillingAddress.Zip),
                    new PaymentDto(order.Payment.CardName,
                    order.Payment.CardNumber,
                    order.Payment.Expiration,
                    order.Payment.CVV,
                    order.Payment.PaymentMethod),
                    order.OrderStatus,
                    order.OrderItems.Select(oi => new OrderItemDto(oi.OrderId.Value, oi.ProductId.Value, oi.Quantity, oi.Price)).ToList());
                orderDtos.Add(orderDto);
            }
            return orderDtos;
        }
    }
}
