namespace IconGeneratorAI.Shared.Models;

public record class CreateIconGenerationRequestDto
{
    public Guid AIModelId { get; set; }
    public List<CreateIconGenerationParameterRequestDto> Parameters { get; set; }
    public string Prompt { get; set; }

    public CreateIconGenerationRequestDto(Guid aIModelId, List<CreateIconGenerationParameterRequestDto> parameters, string prompt)
    {
        AIModelId = aIModelId;
        Parameters = parameters;
        Prompt = prompt;
    }

    public CreateIconGenerationRequestDto()
    {

    }
}
