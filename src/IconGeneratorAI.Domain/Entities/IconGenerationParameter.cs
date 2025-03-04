using IconGeneratorAI.Domain.Common;

namespace IconGeneratorAI.Domain.Entities;

public sealed class IconGenerationParameter : EntityBase
{
    public Guid IconGenerationId { get; set; }
    public IconGeneration IconGeneration { get; set; }

    public Guid AIModelParameterId { get; set; }
    public AIModelParameter AIModelParameter { get; set; }

    public string Value { get; set; } // e.g. "1024x1024"


    public static IconGenerationParameter Create(Guid aIModelParameterId, string value, Guid userId)
    {
        return new IconGenerationParameter()
        {
            Id = Guid.CreateVersion7(),
            AIModelParameterId = aIModelParameterId,
            Value = value,
            CreatedAt = DateTimeOffset.UtcNow,
            CreatedByUserId = userId.ToString(),
        };
    }
}
