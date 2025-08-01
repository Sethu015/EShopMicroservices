﻿namespace Ordering.Domain.ValueObjects
{
    public record OrderId
    {
        public Guid Value {  get; }
        private OrderId(Guid value) => Value = value;

        public static OrderId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value.Equals(Guid.Empty))
            {
                throw new DomainException("OrderId Cannot be Empty");
            }
            return new OrderId(value);
        }
    }
}
