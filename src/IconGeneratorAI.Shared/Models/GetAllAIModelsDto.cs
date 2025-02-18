
namespace IconGeneratorAI.Shared.Models;

public sealed record GetAllAIModelsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public List<GetAllAIModelsAIParameterDto> Parameters { get; set; } = [];

    public GetAllAIModelsDto(Guid id, string name, List<GetAllAIModelsAIParameterDto> parameters)
    {
        Id = id;
        Name = name;
        Parameters = parameters;
    }

    public GetAllAIModelsDto()
    {

    }
}
