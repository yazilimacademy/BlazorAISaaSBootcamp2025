using IconGeneratorAI.Domain.Common;

namespace IconGeneratorAI.Domain.Entities;

public sealed class AIModel : EntityBase
{
    public string Name { get; set; } // e.g. "Flux Schnell"
    public string? Description { get; set; }
    public string ModelUrl { get; set; } // e.g. "black-forest-labs/flux-schnell"

    // Relationship: One AIModel has many parameters
    public ICollection<AIModelParameter> Parameters { get; set; } = [];
}
