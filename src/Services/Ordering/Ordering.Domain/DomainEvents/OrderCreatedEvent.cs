namespace Ordering.Domain.DomainEvents
{
    public record OrderCreatedEvent(Order order) : IDomainEvent;
}
