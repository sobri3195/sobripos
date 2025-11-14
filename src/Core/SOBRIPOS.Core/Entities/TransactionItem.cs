namespace SOBRIPOS.Core.Entities;

public class TransactionItem : BaseEntity
{
    public Guid TransactionId { get; set; }
    public Transaction Transaction { get; set; } = null!;
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal? DiscountPercentage { get; set; }
    public decimal Subtotal { get; set; }
}
