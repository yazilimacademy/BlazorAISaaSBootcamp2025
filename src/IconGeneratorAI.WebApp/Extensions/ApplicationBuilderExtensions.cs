using IconGeneratorAI.Domain.Entities;
using IconGeneratorAI.WebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace IconGeneratorAI.WebApp.Extensions;

public static class ApplicationBuilderExtensions
{
        public static async Task<IApplicationBuilder> ApplyMigrationsAsync(this IApplicationBuilder app)
        {
                CancellationTokenSource cts = new();

                using var scope = app.ApplicationServices.CreateAsyncScope();

                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (dbContext.Database.GetPendingMigrations().Any())
                {
                        await dbContext.Database.MigrateAsync(cts.Token);
                }

                if (!await dbContext.IconResults.AnyAsync(cts.Token))
                {
                        for (int i = 0; i < 10; i++)
                        {
                                var iconResult = IconResult.Create($"Icon {i}", $"Description {i}", $"https://api.dicebear.com/7.x/avataaars/png?seed={i}");

                                await dbContext.IconResults.AddAsync(iconResult, cts.Token);
                        }
                        await dbContext.SaveChangesAsync(cts.Token);
                }

                return app;

        } // TODO()
}
