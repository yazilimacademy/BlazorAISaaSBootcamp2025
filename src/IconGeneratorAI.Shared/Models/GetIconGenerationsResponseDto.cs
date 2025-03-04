namespace IconGeneratorAI.Shared.Models;

public sealed record GetIconGenerationsResponseDto
{
    public Guid Id { get; set; }
    public string Prompt { get; set; }
    public string ImageUrl { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string AIModelName { get; set; }
    public string Style { get; set; }

    public GetIconGenerationsResponseDto(Guid id, string prompt, string imageUrl, DateTimeOffset createdAt, string aiModelName, string style)
    {
        Id = id;
        Prompt = prompt;
        ImageUrl = imageUrl;
        CreatedAt = createdAt;
        AIModelName = aiModelName;
        Style = style;
    }

    public GetIconGenerationsResponseDto()
    {
    }
}