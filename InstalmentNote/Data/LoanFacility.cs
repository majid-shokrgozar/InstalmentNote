using System.ComponentModel.DataAnnotations;

namespace InstalmentNote.Data;

public class LoanFacility
{
    public int Id { get; set; }

    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(150)]
    public string ProviderName { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? FacilityType { get; set; }

    // شماره قرارداد
    [MaxLength(100)]
    public string? ContractNumber { get; set; }

    // مبلغ اصل وام
    public decimal PrincipalAmount { get; set; }

    public decimal? ReceivedAmount { get; set; }

    public decimal? InterestRate { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public int InstallmentCount { get; set; }

    public decimal? InstallmentAmount { get; set; }

    [MaxLength(1000)]
    public string? Description { get; set; }

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    [Required]
    public string UserId { get; set; } = string.Empty;

    public ApplicationUser User { get; set; } = null!;

    public ICollection<Installment> Installments { get; set; } = new List<Installment>();
}
