using System;
using IconGeneratorAI.WebApp.Models;

namespace IconGeneratorAI.WebApp.Data;

public static class FakeDb
{
    private static List<Article> Articles { get; set; } = new List<Article>(){
        new Article(){
            Id = Guid.CreateVersion7(),
            Title = "Article 1",
            Content = "Article 1 Content",
            CreatedAt = DateTime.Now
        },
        new Article(){
            Id = Guid.CreateVersion7(),
            Title = "Article 2",
            Content = "Article 2 Content",
            CreatedAt = DateTime.Now
        },
        new Article(){
            Id = Guid.CreateVersion7(),
            Title = "Article 3",
            Content = "Article 3 Content",
            CreatedAt = DateTime.Now
        },
        new Article(){
            Id = Guid.CreateVersion7(),
            Title = "Article 4",
            Content = "Article 4 Content",
            CreatedAt = DateTime.Now
        },
        new Article(){
            Id = Guid.CreateVersion7(),
            Title = "Article 5",
            Content = "Article 5 Content",
            CreatedAt = DateTime.Now
        }
    };

    public static List<Article> GetArticles()
    {
        return Articles;
    }
}
