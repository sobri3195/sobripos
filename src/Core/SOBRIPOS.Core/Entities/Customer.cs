namespace SOBRIPOS.Core.Entities;

public class Customer : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public int LoyaltyPoints { get; set; }
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
