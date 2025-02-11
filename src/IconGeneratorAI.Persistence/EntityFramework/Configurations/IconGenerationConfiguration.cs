using System;
using IconGeneratorAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IconGeneratorAI.Persistence.EntityFramework.Configurations;

public sealed class IconGenerationConfiguration : IEntityTypeConfiguration<IconGeneration>
{
       public void Configure(EntityTypeBuilder<IconGeneration> builder)
       {
              // ID - Primary Key
              builder.HasKey(x => x.Id);
              builder.Property(x => x.Id).ValueGeneratedNever(); // Eğer ID'yi kendiniz oluşturuyorsanız ValueGeneratedNever(), otomatik oluşuyorsa ValueGeneratedOnAdd() kullanın.

              // Relationships
              builder.HasOne(x => x.AIModel)
                     .WithMany() // Eğer AIModel entity'sinde IconGeneration'a geri dönen bir navigation property yoksa WithMany() kullanın. Varsa ona göre ayarlayın.
                     .HasForeignKey(x => x.AIModelId)
                     .IsRequired()
                     .OnDelete(DeleteBehavior.Restrict);

              builder.HasOne(x => x.User)
                     .WithMany() // Eğer ApplicationUser entity'sinde IconGeneration'a geri dönen bir navigation property yoksa WithMany() kullanın. Varsa ona göre ayarlayın.
                     .HasForeignKey(x => x.UserId)
                     .IsRequired()
                     .OnDelete(DeleteBehavior.Restrict);

              builder.HasMany(x => x.Parameters)
                     .WithOne(x => x.IconGeneration)
                     .HasForeignKey(x => x.IconGenerationId)
                     .OnDelete(DeleteBehavior.Cascade);

              // Properties
              builder.Property(x => x.Prompt)
                     .IsRequired()
                     .HasMaxLength(1000); // Örnek olarak MaxLength belirledim, ihtiyaca göre ayarlayın.


              builder.Property(x => x.ImageUrl)
                     .IsRequired(false) // ImageUrl opsiyonel olduğu için IsRequired(false)
                     .HasMaxLength(2000); // Örnek olarak MaxLength belirledim, ihtiyaca göre ayarlayın. URL'ler uzun olabilir.

              builder.Property(x => x.PrimaryColor)
                     .IsRequired(false) // PrimaryColor opsiyonel olduğu için IsRequired(false)
                     .HasMaxLength(10); // Hex kodları için yeterli uzunluk.

              builder.Property(x => x.GenerationTime)
                     .IsRequired(false); // GenerationTime opsiyonel olduğu için IsRequired(false). TimeSpan? zaten null olabilir.

              // Enum Özellikler ve Veritabanı Tipleri
              builder.Property(x => x.Style)
                     .HasConversion<int>() // Enum'ı int olarak veritabanına kaydet
                     .HasColumnType("SMALLINT") // Veritabanı sütun tipini SMALLINT olarak ayarla
                     .IsRequired();

              // Common Properties (EntityBase'den gelenler)
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

              builder.ToTable("icon_generations"); // Veritabanı tablo adını belirtin
       }


}
