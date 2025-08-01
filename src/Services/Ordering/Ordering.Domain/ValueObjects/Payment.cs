﻿namespace Ordering.Domain.ValueObjects
{
    public record Payment
    {
        public string? CardName { get; } = default!;
        public string CardNumber { get; } = default!;
        public string Expiration {  get; } = default!;
        public string CVV { get; } = default!;
        public int PaymentMethod { get; } = default!;

        private Payment(string cardName, string cardNumber,  string expiration, string cvv, int paymentMethod)
        {
            CardName = cardName;
            CardNumber = cardNumber;
            Expiration = expiration;
            CVV = cvv;
            PaymentMethod = paymentMethod;
        }

        protected Payment()
        {

        }

        public static Payment Of(string cardName, string cardNumber, string expiration, string cvv, int paymentMethod)
        {
            return new Payment(cardName, cardNumber, expiration, cvv, paymentMethod);
        }
    }
}
