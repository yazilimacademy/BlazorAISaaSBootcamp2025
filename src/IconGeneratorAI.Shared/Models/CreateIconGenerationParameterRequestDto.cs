namespace IconGeneratorAI.Shared.Models;

public sealed record CreateIconGenerationParameterRequestDto
{
    public Guid Id { get; set; }
    public string Value { get; set; }
}
