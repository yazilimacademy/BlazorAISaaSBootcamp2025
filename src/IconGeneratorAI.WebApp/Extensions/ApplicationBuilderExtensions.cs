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

                        await Task.Delay(3000);

                        var userBalance = UserBalance.Create(userId, 100);

                        await dbContext.UserBalances.AddAsync(userBalance, cts.Token);
                        await dbContext.SaveChangesAsync(cts.Token);

                        transactionScope.Complete();
                }


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
