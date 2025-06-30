using RideHailing.Domain.Enums;

namespace RideHailing.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public UserRole Role { get; set; }
        public decimal WalletBalance { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<Trip> Trips { get; set; }
        public ICollection<PaymentMethod> PaymentMethods { get; set; }

    }
}
