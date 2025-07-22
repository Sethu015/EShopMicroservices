namespace Ordering.Domain.ValueObjects
{
    public record Address
    {
        public string FirstName { get; } = default!;
        public string LastName { get; } = default!;
        public string? EmailAddress { get; } = default!;
        public string AddressLine {  get; } = default!;
        public string Country { get; } = default!;
        public string State { get; } = default!;
        public string Zip {  get; } = default!;

        private Address(string firstName, string lastName, string emailAddress, string addressLine, string country, string state, string zip)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            AddressLine = addressLine;
            Country = country;
            State = state;
            Zip = zip;
        }

        protected Address()
        {

        }

        public static Address Of(string firstName, string lastName, string emailAddress, string addressLine, string country, string state, string zip)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(emailAddress);
            ArgumentException.ThrowIfNullOrWhiteSpace(addressLine);
            return new Address(firstName, lastName, emailAddress, addressLine, country, state, zip);
        }

    }
}
