using System;
using IconGeneratorAI.Domain.Common;
using IconGeneratorAI.Domain.Enums;

namespace IconGeneratorAI.Domain.Entities;

public class AIModelParameter : EntityBase
{
    public Guid AIModelId { get; set; }
    public AIModel AIModel { get; set; }

    // E.g. "prompt", "go_fast", "aspect_ratio"
    public string Name { get; set; }

    // E.g. "Prompt Text", "Go Fast?", "Aspect Ratio"
    public string DisplayName { get; set; }

    // ParameterType: String, Integer, Boolean, Float, Enum
    public AIModelParameterType Type { get; set; }

    public bool IsRequired { get; set; }

    // Optional default value
    public string DefaultValue { get; set; }

    // For enumerated or select-like parameters, or to store hints
    // e.g. [ "1:1", "16:9", "9:16" ] or [ "prompt", "size" ] 
    public List<string> PossibleValues { get; set; } = [];

    public ICollection<IconGenerationParameter> IconGenerationParameters { get; set; } = [];
}

