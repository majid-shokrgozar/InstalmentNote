using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InstalmentNote.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<LoanFacility> LoanFacilities => Set<LoanFacility>();

    public DbSet<Installment> Installments => Set<Installment>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<LoanFacility>(entity =>
        {
            entity.ToTable("LoanFacilities");

            entity.Property(loan => loan.Title)
                .HasMaxLength(200);

            entity.Property(loan => loan.ProviderName)
                .HasMaxLength(150);

            entity.Property(loan => loan.FacilityType)
                .HasMaxLength(100);

            entity.Property(loan => loan.ContractNumber)
                .HasMaxLength(100);

            entity.Property(loan => loan.PrincipalAmount)
                .HasColumnType("decimal(18,2)");

            entity.Property(loan => loan.ReceivedAmount)
                .HasColumnType("decimal(18,2)");

            entity.Property(loan => loan.InterestRate)
                .HasColumnType("decimal(5,2)");

            entity.Property(loan => loan.InstallmentAmount)
                .HasColumnType("decimal(18,2)");

            entity.Property(loan => loan.Description)
                .HasMaxLength(1000);

            entity.Property(loan => loan.CreatedAtUtc)
                .HasDefaultValueSql("NOW()");

            entity.HasOne(loan => loan.User)
                .WithMany(user => user.LoanFacilities)
                .HasForeignKey(loan => loan.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(loan => new { loan.UserId, loan.ProviderName, loan.StartDate });
        });

        builder.Entity<Installment>(entity =>
        {
            entity.ToTable("Installments");

            entity.Property(installment => installment.Amount)
                .HasColumnType("decimal(18,2)");

            entity.Property(installment => installment.Note)
                .HasMaxLength(500);

            entity.Property(installment => installment.CreatedAtUtc)
                .HasDefaultValueSql("NOW()");

            entity.HasOne(installment => installment.LoanFacility)
                .WithMany(loanFacility => loanFacility.Installments)
                .HasForeignKey(installment => installment.LoanFacilityId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(installment => new { installment.LoanFacilityId, installment.SequenceNumber })
                .IsUnique();

            entity.HasIndex(installment => new { installment.DueDate, installment.PaymentStatus, installment.DueStatus });
        });
    }
}
