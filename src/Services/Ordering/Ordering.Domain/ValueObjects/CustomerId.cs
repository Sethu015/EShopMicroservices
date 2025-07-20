namespace Ordering.Domain.ValueObjects
{
    public record CustomerId
    {
        public Guid Value { get; }
        private CustomerId(Guid value) => Value = value;

        public static CustomerId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value.Equals(Guid.Empty))
            {
                throw new DomainException("CustomerId Cannot be Empty");
            }

            return new CustomerId(value);
        }
    }
}
