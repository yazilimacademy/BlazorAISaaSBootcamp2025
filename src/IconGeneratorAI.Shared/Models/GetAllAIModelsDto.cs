
namespace IconGeneratorAI.Shared.Models;

public sealed record GetAllAIModelsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ModelUrl { get; set; }
    public List<string> Parameters { get; set; } = [];

    public GetAllAIModelsDto(Guid id, string name, string description, string modelUrl, List<string> parameters)
    {
        Id = id;
        Name = name;
        Description = description;
        ModelUrl = modelUrl;
        Parameters = parameters;
    }

    public GetAllAIModelsDto()
    {

    }
}
