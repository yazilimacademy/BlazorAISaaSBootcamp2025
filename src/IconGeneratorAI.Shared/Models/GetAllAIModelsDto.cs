
namespace IconGeneratorAI.Shared.Models;

public sealed record GetAllAIModelsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<string> Sizes { get; set; } = [];

    public GetAllAIModelsDto(Guid id, string name, List<string> sizes)
    {
        Id = id;
        Name = name;
        Sizes = sizes;
    }

    public GetAllAIModelsDto()
    {

    }
}
