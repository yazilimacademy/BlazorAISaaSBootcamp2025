using System;

namespace IconGeneratorAI.Domain.Entities;

public sealed class IconResult
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string Url { get; set; }
    public DateTimeOffset CreatedAt { get; set; }


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
