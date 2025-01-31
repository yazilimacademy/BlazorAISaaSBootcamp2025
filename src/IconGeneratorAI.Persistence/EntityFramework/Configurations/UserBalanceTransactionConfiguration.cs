using IconGeneratorAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IconGeneratorAI.Persistence.EntityFramework.Configurations;

public sealed class UserBalanceTransactionConfiguration : IEntityTypeConfiguration<UserBalanceTransaction>
{
    public void Configure(EntityTypeBuilder<UserBalanceTransaction> builder)
    {
        // ID - Primary Key
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd(); // Identity - Auto Increment

        builder.Property(x => x.Amount)
        .IsRequired();

        builder.Property(x => x.BalanceAfterTransaction)
        .IsRequired();

        builder.Property(x => x.Description)
        .IsRequired(false)
        //.HasColumnType("nvarchar(500)")
        .HasMaxLength(500); // nvarchar(500)

        // builder.HasOne<UserBalance>(x => x.UserBalance)
        // .WithMany(y => y.Transactions)
        // .HasForeignKey(y => y.UserBalanceId);

        builder.Property(x => x.Type) // 0,1,2 // Add, Remove
        .HasConversion<int>()
        .HasColumnType("SMALLINT") // 0,255
        .IsRequired();

        // Common Properties
        builder.Property(x => x.CreatedAt)
        .IsRequired();

        builder.Property(x => x.CreatedByUserId)
        .HasMaxLength(100)
        .IsRequired();

        builder.Property(x => x.UpdatedAt)
        .IsRequired(false);

        builder.Property(x => x.UpdatedByUserId)
        .HasMaxLength(100)
        .IsRequired(false);

        builder.ToTable("user_balance_transactions");
    }
}
