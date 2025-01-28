using IconGeneratorAI.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IconGeneratorAI.Persistence.EntityFramework.Configurations;

public sealed class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        //Id
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        // Indexes for "normalized" username and email, to allow efficient lookups
        builder.HasIndex(u => u.NormalizedUserName).HasDatabaseName("UserNameIndex").IsUnique();
        builder.HasIndex(u => u.NormalizedEmail).HasDatabaseName("EmailIndex");

        // A concurrency token for use with the optimistic concurrency checking
        builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

        // Limit the size of columns to use efficient database types
        builder.Property(u => u.UserName).HasMaxLength(100);
        builder.Property(u => u.NormalizedUserName).HasMaxLength(100);

        //Email
        builder.Property(u => u.Email).IsRequired();
        builder.HasIndex(user => user.Email).IsUnique();
        builder.Property(u => u.Email).HasMaxLength(150);
        builder.Property(u => u.NormalizedEmail).HasMaxLength(150);

        //PhoneNumber
        builder.Property(u => u.PhoneNumber).IsRequired(false);
        builder.Property(u => u.PhoneNumber).HasMaxLength(20);

        //FullName
        builder.Property(u => u.FullName).IsRequired();
        builder.Property(u => u.FullName).HasMaxLength(100);


        // //FirstName
        // builder.Property(u => u.FirstName).IsRequired();
        // builder.Property(u => u.FirstName).HasMaxLength(100);

        // //LastName
        // builder.Property(u => u.LastName).IsRequired();
        // builder.Property(u => u.LastName).HasMaxLength(100);


        // The relationships between User and other entity types
        // Note that these relationships are configured with no navigation properties

        // Each User can have many UserClaims
        builder.HasMany<ApplicationUserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

        // Each User can have many UserLogins
        builder.HasMany<ApplicationUserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

        // Each User can have many UserTokens
        builder.HasMany<ApplicationUserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

        // Each User can have many entries in the UserRole join table
        builder.HasMany<ApplicationUserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();

        // Relationships
        // Common Properties

        // Common fields
        // CreatedOn
        builder.Property(p => p.CreatedAt)
            .IsRequired();

        // CreatedByUserId
        builder.Property(p => p.CreatedByUserId)
            .IsRequired(false)
            .HasMaxLength(150);

        // ModifiedOn
        builder.Property(p => p.UpdatedAt)
            .IsRequired(false);

        // UpdatedByUserId
        builder.Property(p => p.UpdatedByUserId)
            .IsRequired(false)
            .HasMaxLength(150);

        builder.ToTable("application_users");
    }
}