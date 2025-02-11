using IconGeneratorAI.Domain.Entities;
using IconGeneratorAI.Domain.Enums;
using IconGeneratorAI.Persistence.EntityFramework.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IconGeneratorAI.Persistence.EntityFramework.Configurations;

public sealed class AIModelParameterConfiguration : IEntityTypeConfiguration<AIModelParameter>
{
    public void Configure(EntityTypeBuilder<AIModelParameter> builder)
    {
        // Primary Key
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        // Relationships
        builder.HasOne(x => x.AIModel)
               .WithMany(x => x.Parameters)
               .HasForeignKey(x => x.AIModelId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);

        // Properties
        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(x => x.DisplayName)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(x => x.Type)
               .HasConversion<int>()
               .HasColumnType("SMALLINT")
               .IsRequired();

        builder.Property(x => x.IsRequired)
               .IsRequired();

        builder.Property(x => x.DefaultValue)
               .HasMaxLength(1000)
               .IsRequired(false);

        // Value Converter for PossibleValues List<string>
        var converter = new JsonListStringConverter();
        builder.Property(x => x.PossibleValues)
               .HasConversion(converter)
               .HasColumnType("jsonb")
               .IsRequired();

        builder.HasIndex(x => x.PossibleValues)
               .HasMethod("GIN");

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

        builder.ToTable("ai_model_parameters");
    }
}