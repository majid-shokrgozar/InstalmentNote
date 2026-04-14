using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InstalmentNote.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Instalment> Instalments => Set<Instalment>();

    public DbSet<InstalmentItem> InstalmentItems => Set<InstalmentItem>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Instalment>(entity =>
        {
            entity.ToTable("Instalments");

            entity.Property(instalment => instalment.Title)
                .HasMaxLength(200);

            entity.Property(instalment => instalment.BankName)
                .HasMaxLength(150);

            entity.Property(instalment => instalment.ContractNumber)
                .HasMaxLength(100);

            entity.Property(instalment => instalment.AccountNumber)
                .HasMaxLength(100);

            entity.Property(instalment => instalment.TotalAmount)
                .HasColumnType("decimal(18,2)");

            entity.Property(instalment => instalment.InstalmentAmount)
                .HasColumnType("decimal(18,2)");

            entity.Property(instalment => instalment.Description)
                .HasMaxLength(1000);

            entity.Property(instalment => instalment.CreatedAtUtc)
                .HasDefaultValueSql("GETUTCDATE()");

            entity.HasOne(instalment => instalment.User)
                .WithMany(user => user.Instalments)
                .HasForeignKey(instalment => instalment.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(instalment => new { instalment.UserId, instalment.BankName, instalment.StartDate });
        });

        builder.Entity<InstalmentItem>(entity =>
        {
            entity.ToTable("InstalmentItems");

            entity.Property(item => item.Amount)
                .HasColumnType("decimal(18,2)");

            entity.Property(item => item.Note)
                .HasMaxLength(500);

            entity.Property(item => item.CreatedAtUtc)
                .HasDefaultValueSql("GETUTCDATE()");

            entity.HasOne(item => item.Instalment)
                .WithMany(instalment => instalment.Items)
                .HasForeignKey(item => item.InstalmentId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(item => new { item.InstalmentId, item.SequenceNumber })
                .IsUnique();

            entity.HasIndex(item => new { item.DueDate, item.PaymentStatus, item.DueStatus });
        });
    }
}
