using IconGeneratorAI.Domain.Common;

namespace IconGeneratorAI.Domain.Entities;

public sealed class IconGenerationParameter : EntityBase
{
    public Guid IconGenerationId { get; set; }
    public IconGeneration IconGeneration { get; set; }

    public Guid AIModelParameterId { get; set; }
    public AIModelParameter AIModelParameter { get; set; }

    public string Value { get; set; } // e.g. "1024x1024"
}
