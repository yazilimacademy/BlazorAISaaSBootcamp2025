using System;

namespace IconGeneratorAI.WebApp.Models;

public sealed class Article
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
}
