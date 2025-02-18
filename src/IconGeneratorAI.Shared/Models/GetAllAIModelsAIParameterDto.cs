using IconGeneratorAI.Shared.Enums;

namespace IconGeneratorAI.Shared.Models;

public sealed record GetAllAIModelsAIParameterDto
{
    public Guid Id { get; set; }
    public Guid AIModelId { get; set; }
    public string DisplayName { get; set; }

    // ParameterType: String, Integer, Boolean, Float, Enum
    public AIModelParameterType Type { get; set; }

    public bool IsRequired { get; set; }

    // Optional default value
    public string DefaultValue { get; set; }

    // For enumerated or select-like parameters, or to store hints
    // e.g. [ "1:1", "16:9", "9:16" ] or [ "prompt", "size" ] 
    public List<string> PossibleValues { get; set; } = [];

    public GetAllAIModelsAIParameterDto(Guid id, Guid aiModelId, string displayName, AIModelParameterType type, bool isRequired, string defaultValue, List<string> possibleValues)
    {
        Id = id;
        AIModelId = aiModelId;
        DisplayName = displayName;
        Type = type;
        IsRequired = isRequired;
        DefaultValue = defaultValue;
        PossibleValues = possibleValues;
    }

    public GetAllAIModelsAIParameterDto()
    {

    }
}
