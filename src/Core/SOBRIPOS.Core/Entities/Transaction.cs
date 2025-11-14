using SOBRIPOS.Core.Enums;

namespace SOBRIPOS.Core.Entities;

public class Transaction : BaseEntity
{
    public string TransactionNumber { get; set; } = string.Empty;
    public DateTime TransactionDate { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal? DiscountAmount { get; set; }
    public decimal? TaxAmount { get; set; }
    public decimal FinalAmount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public TransactionStatus Status { get; set; }
    public Guid CashierId { get; set; }
    public User Cashier { get; set; } = null!;
    public Guid? CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public ICollection<TransactionItem> Items { get; set; } = new List<TransactionItem>();
}
