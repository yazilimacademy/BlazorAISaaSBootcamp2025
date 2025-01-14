using System;

namespace IconGeneratorAI.Domain.Entities;

public sealed class IconResult
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public string Url { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }


    public static IconResult Create(string title, string? description, string url)
    {

        return new IconResult
        {
            Id = Guid.CreateVersion7(),
            Title = title,
            Description = description,
            Url = url,
            CreatedAt = DateTimeOffset.UtcNow
        };
    }


}
