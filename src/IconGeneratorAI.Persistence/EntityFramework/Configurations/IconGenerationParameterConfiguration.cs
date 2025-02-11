using IconGeneratorAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IconGeneratorAI.Persistence.EntityFramework.Configurations;

public sealed class IconGenerationParameterConfiguration : IEntityTypeConfiguration<IconGenerationParameter>
{
    public void Configure(EntityTypeBuilder<IconGenerationParameter> builder)
    {
        // Primary Key
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        // Relationships
        builder.HasOne(x => x.IconGeneration)
               .WithMany(x => x.Parameters)
               .HasForeignKey(x => x.IconGenerationId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.AIModelParameter)
               .WithMany(x => x.IconGenerationParameters)
               .HasForeignKey(x => x.AIModelParameterId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);

        // Properties
        builder.Property(x => x.Value)
               .IsRequired()
               .HasMaxLength(1000);

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

        builder.ToTable("icon_generation_parameters");
    }
}