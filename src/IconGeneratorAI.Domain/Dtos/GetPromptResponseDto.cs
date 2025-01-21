namespace IconGeneratorAI.Domain.Dtos;

public sealed record GetPromptResponseDto
{
    public string ImprovedPrompt { get; set; }

    public GetPromptResponseDto(string improvedPrompt)
    {
        ImprovedPrompt = improvedPrompt;
    }

    public GetPromptResponseDto()
    {

    }
}
