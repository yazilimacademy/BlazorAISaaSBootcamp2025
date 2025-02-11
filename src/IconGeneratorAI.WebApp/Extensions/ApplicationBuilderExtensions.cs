using System.Transactions;
using IconGeneratorAI.Domain.Entities;
using IconGeneratorAI.Domain.Identity;
using IconGeneratorAI.Domain.ValueObjects;
using IconGeneratorAI.Persistence.EntityFramework.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IconGeneratorAI.WebApp.Extensions;

public static class ApplicationBuilderExtensions
{
        public static async Task<IApplicationBuilder> ApplyMigrationsAsync(this IApplicationBuilder app)
        {
                CancellationTokenSource cts = new();

                using var scope = app.ApplicationServices.CreateAsyncScope();

                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                if (dbContext.Database.GetPendingMigrations().Any())
                {
                        await dbContext.Database.MigrateAsync(cts.Token);
                }

                // Seed default user if no users exist
                if (!await userManager.Users.AnyAsync(cts.Token))
                {

                        using TransactionScope transactionScope = new(TransactionScopeAsyncFlowOption.Enabled);

                        var userId = Guid.CreateVersion7();

                        var defaultUser = new ApplicationUser
                        {
                                Id = userId,
                                UserName = "alper.hoca@example.com",
                                Email = "alper.hoca@example.com",
                                EmailConfirmed = true,
                                FullName = FullName.Create("Alper", "Hoca"),
                                CreatedAt = DateTimeOffset.UtcNow,
                                CreatedByUserId = userId.ToString(),
                        };

                        var result = await userManager.CreateAsync(defaultUser, "Password123!");
                        if (!result.Succeeded)
                        {
                                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                                throw new Exception($"Failed to create default user: {errors}");
                        }

                        var userBalance = UserBalance.Create(userId, 100);

                        await dbContext.UserBalances.AddAsync(userBalance, cts.Token);
                        await dbContext.SaveChangesAsync(cts.Token);

                        transactionScope.Complete();
                }

                var user = await userManager
                .FindByEmailAsync("alper.hoca@example.com") ?? throw new Exception("User not found");

                // // Seed AIModel if no models exist
                // if (!await dbContext.AIModels.AnyAsync(cts.Token))
                // {
                //         var recraftModel = new AIModel
                //         {
                //                 Id = Guid.CreateVersion7(), // Set the Id using Guid.CreateVersion7()
                //                 Name = "Recraft",
                //                 Description = "Recraft V3 (recraft-v3) is a text-to-image model with the ability to generate long texts, and images in a wide list of styles. As of today, it is SOTA in image generation, proven by the Text-to-Image Benchmark by Artificial Analysis.",
                //                 ModelUrl = "recraft-ai/recraft-v3",
                //                 Parameters = [
                //                         "1365x1024", "1024x1024", "1365x1024", "1024x1365", "1536x1024",
                //         "1024x1536", "1820x1024", "1024x1820", "1024x2048", "2048x1024",
                //         "1434x1024", "1024x1434", "1024x1280", "1280x1024", "1024x1707",
                //                 CreatedAt = DateTimeOffset.UtcNow,
                //                 CreatedByUserId = user.Id.ToString(),
                //         };



                //         await dbContext.AIModels.AddAsync(recraftModel, cts.Token);
                //         await dbContext.SaveChangesAsync(cts.Token);
                // }


                // if (!await dbContext.IconResults.AnyAsync(cts.Token))
                // {
                //         for (int i = 0; i < 10; i++)
                //         {
                //                 var iconResult = IconResult.Create($"Icon {i}", $"Description {i}", $"https://api.dicebear.com/7.x/avataaars/png?seed={i}");

                //                 await dbContext.IconResults.AddAsync(iconResult, cts.Token);
                //         }
                //         await dbContext.SaveChangesAsync(cts.Token);
                // }

                return app;
        } // TODO()
}
