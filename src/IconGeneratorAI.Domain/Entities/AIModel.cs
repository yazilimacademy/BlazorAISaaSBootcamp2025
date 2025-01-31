using IconGeneratorAI.Domain.Common;

namespace IconGeneratorAI.Domain.Entities;

public sealed class AIModel : EntityBase
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string ModelUrl { get; set; }

    public List<string> Sizes { get; set; } = [];
}
