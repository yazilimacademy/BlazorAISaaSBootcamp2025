namespace IconGeneratorAI.Shared.Models;

public record class CreateIconGenerationRequestDto
{
    public Guid AIModelId { get; set; }
    public List<CreateIconGenerationParameterRequestDto> Parameters { get; set; }

    public CreateIconGenerationRequestDto(Guid aIModelId, List<CreateIconGenerationParameterRequestDto> parameters)
    {
        AIModelId = aIModelId;
        Parameters = parameters;
    }

    public CreateIconGenerationRequestDto()
    {

    }
}
