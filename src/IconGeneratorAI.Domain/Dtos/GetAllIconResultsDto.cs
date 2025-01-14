namespace IconGeneratorAI.Domain.Dtos;

public sealed record GetAllIconResultsDto
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string? Description { get; init; }
    public string Url { get; init; }
    public DateTimeOffset CreatedAt { get; init; }

    public GetAllIconResultsDto(Guid id, string title, string? description, string url, DateTimeOffset createdAt)
    {
        Id = id;
        Title = title;
        Description = description;
        Url = url;
        CreatedAt = createdAt;
    }

    public GetAllIconResultsDto()
    {

    }
};
