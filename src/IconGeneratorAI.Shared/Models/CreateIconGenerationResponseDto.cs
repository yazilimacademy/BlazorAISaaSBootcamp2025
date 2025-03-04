namespace IconGeneratorAI.Shared.Models;

public sealed record CreateIconGenerationResponseDto
{
    public Guid Id { get; set; }
    public string ImageUrl { get; set; }

    public CreateIconGenerationResponseDto(Guid id, string imageUrl)
    {
        Id = id;
        ImageUrl = imageUrl;
    }

    public CreateIconGenerationResponseDto()
    {

    }
}
