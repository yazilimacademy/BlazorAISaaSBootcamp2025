using System;
using IconGeneratorAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IconGeneratorAI.Persistence.EntityFramework.Configurations;

public sealed class UserBalanceConfiguration : IEntityTypeConfiguration<UserBalance>
{
       public void Configure(EntityTypeBuilder<UserBalance> builder)
       {
              // ID - Primary Key
              builder.HasKey(x => x.UserId);

              // Relationships
              builder.HasOne(x => x.User)
                     .WithOne(x => x.UserBalance)
                     .HasForeignKey<UserBalance>(x => x.UserId);


              // Properties
              builder.Property(x => x.Balance)
                     .IsRequired();

              builder.HasMany<UserBalanceTransaction>(x => x.Transactions)
              .WithOne(y => y.UserBalance)
              .HasForeignKey(y => y.UserBalanceId);


              // Common Properties
              builder.Property(x => x.CreatedAt) // created_at
                     .IsRequired();

              builder.Property(x => x.CreatedByUserId)
                     .HasMaxLength(100)
                     .IsRequired();

              builder.Property(x => x.UpdatedAt)
                     .IsRequired(false);

              builder.Property(x => x.UpdatedByUserId)
                     .HasMaxLength(100)
                     .IsRequired(false);

              builder.ToTable("user_balances");
       }
}
