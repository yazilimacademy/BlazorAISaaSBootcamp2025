using System;
using IconGeneratorAI.Domain.Entities;
using IconGeneratorAI.Persistence.EntityFramework.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IconGeneratorAI.Persistence.EntityFramework.Configurations;

public sealed class AIModelConfiguration : IEntityTypeConfiguration<AIModel>
{
    public void Configure(EntityTypeBuilder<AIModel> builder)
    {
        // ID - Primary Key
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
        .HasMaxLength(250)
               .IsRequired();

        builder.Property(x => x.Description)
               .HasMaxLength(1000)
               .IsRequired(false);

        builder.Property(x => x.ModelUrl)
               .HasMaxLength(1000)
               .IsRequired();

        // Value Converter'ı burada kullanıyoruz
        var converter = new JsonListStringConverter();

        builder.Property(x => x.Sizes)
               .HasConversion(converter)
               .HasColumnType("jsonb")
               .IsRequired();

        builder.HasIndex(x => x.Sizes)
        .HasMethod("GIN");

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

        builder.ToTable("ai_models");
    }
}
