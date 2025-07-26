namespace Ordering.Domain.DomainEvents
{
    public record OrderUpdatedEvent (Order order) : IDomainEvent
    {
    }
}
