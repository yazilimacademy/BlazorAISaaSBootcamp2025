using IconGeneratorAI.Domain.Common;
using IconGeneratorAI.Domain.Enums;
using IconGeneratorAI.Domain.Identity;

namespace IconGeneratorAI.Domain.Entities;

public sealed class IconGeneration : EntityBase
{
    public Guid AIModelId { get; set; }
    public AIModel AIModel { get; set; }
    public IconStyle Style { get; set; }
    public string Prompt { get; set; }
    public string? ImageUrl { get; set; }
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; }

    // Yeni eklenen özellik: Birincil Renk (Hex Kodu olarak)
    public string? PrimaryColor { get; set; }
    public TimeSpan? GenerationTime { get; set; }

    public ICollection<IconGenerationParameter> Parameters { get; set; } = [];

    public static IconGeneration Create(Guid userId, Guid aIModelId, string prompt, string imageUrl)
    {
        return new IconGeneration()
        {
            Id = Guid.CreateVersion7(),
            UserId = userId,
            AIModelId = aIModelId,
            Prompt = prompt,
            ImageUrl = imageUrl,
            CreatedAt = DateTimeOffset.UtcNow,
            CreatedByUserId = userId.ToString(),
            Style = IconStyle.Woodcut,
        };
    }

}
