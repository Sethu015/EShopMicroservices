
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Exception;

namespace Ordering.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler(IApplicationDbContext context)
        : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
    {
        public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            var order = await context.Orders
                .FindAsync([OrderId.Of(command.Id)], cancellationToken);

            if(order is null)
                throw new OrderNotFoundException(command.Id);

            context.Orders.Remove(order);
            await context.SaveChangesAsync(cancellationToken);
            return new DeleteOrderResult(true);
        }
    }
}
