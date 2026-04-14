using System.ComponentModel.DataAnnotations;

namespace InstalmentNote.Data;

public class InstalmentItem
{
    public int Id { get; set; }

    public int InstalmentId { get; set; }

    public Instalment Instalment { get; set; } = null!;

    public int SequenceNumber { get; set; }

    public decimal Amount { get; set; }

    public DateOnly DueDate { get; set; }

    public DateOnly? PaymentDate { get; set; }

    public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;

    public DueStatus DueStatus { get; set; } = DueStatus.Upcoming;

    [MaxLength(500)]
    public string? Note { get; set; }

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
