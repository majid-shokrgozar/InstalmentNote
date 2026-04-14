using System.ComponentModel.DataAnnotations;

namespace InstalmentNote.Data;

public class Instalment
{
    public int Id { get; set; }

    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(150)]
    public string BankName { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? ContractNumber { get; set; }

    [MaxLength(100)]
    public string? AccountNumber { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly FirstDueDate { get; set; }

    public int InstalmentCount { get; set; }

    public decimal TotalAmount { get; set; }

    public decimal InstalmentAmount { get; set; }

    [MaxLength(1000)]
    public string? Description { get; set; }

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    [Required]
    public string UserId { get; set; } = string.Empty;

    public ApplicationUser User { get; set; } = null!;

    public ICollection<InstalmentItem> Items { get; set; } = new List<InstalmentItem>();
}
