namespace RideHailing.Domain.Entities
{
    public class PaymentMethod
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Provider { get; set; } = default!;
        public string? Last4 { get; set; }
        public string? Token { get; set; }
        public bool IsDefault { get; set; }
        public User? User { get; set; }

    }
}
