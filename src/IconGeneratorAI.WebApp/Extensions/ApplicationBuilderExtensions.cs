using System.Transactions;
using IconGeneratorAI.Domain.Entities;
using IconGeneratorAI.Domain.Enums;
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

                // Seed AIModel if no models exist
                if (!await dbContext.AIModels.AnyAsync(cts.Token))
                {
                        var recraftModelId = Guid.CreateVersion7();

                        var recraftModel = new AIModel
                        {
                                Id = recraftModelId, // Set the Id using Guid.CreateVersion7()
                                Name = "Recraft",
                                Description = "Recraft V3 (recraft-v3) is a text-to-image model with the ability to generate long texts, and images in a wide list of styles. As of today, it is SOTA in image generation, proven by the Text-to-Image Benchmark by Artificial Analysis.",
                                ModelUrl = "recraft-ai/recraft-v3",
                                Parameters = [
                                        new AIModelParameter
                                        {
                                                Id = Guid.CreateVersion7(),
                                                Name = "size",
                                                DisplayName = "Size",
                                                Type = AIModelParameterType.String,
                                                IsRequired = true,
                                                DefaultValue = "1024x1024",
                                                PossibleValues = ["1365x1024",
    "1024x1024",
    "1365x1024",
    "1024x1365",
    "1536x1024",
    "1024x1536",
    "1820x1024",
    "1024x1820",
    "1024x2048",
    "2048x1024",
    "1434x1024",
    "1024x1434",
    "1024x1280",
    "1280x1024",
                                                "1024x1707",
                                                "1707x1024"],
                                                AIModelId = recraftModelId,
                                                CreatedAt = DateTimeOffset.UtcNow,
                                                CreatedByUserId = user.Id.ToString(),
                                        },
                                        new AIModelParameter
                                        {
                                                Id = Guid.CreateVersion7(),
                                                Name = "style",
                                                DisplayName = "Style",
                                                Type = AIModelParameterType.String,
                                                IsRequired = true,
                                                DefaultValue = "any",
                                                PossibleValues = [ "any",
    "realistic_image",
    "digital_illustration",
    "digital_illustration/pixel_art",
    "digital_illustration/hand_drawn",
    "digital_illustration/grain",
    "digital_illustration/infantile_sketch",
    "digital_illustration/2d_art_poster",
    "digital_illustration/handmade_3d",
    "digital_illustration/hand_drawn_outline",
    "digital_illustration/engraving_color",
    "digital_illustration/2d_art_poster_2",
    "realistic_image/b_and_w",
    "realistic_image/hard_flash",
    "realistic_image/hdr",
    "realistic_image/natural_light",
    "realistic_image/studio_portrait",
    "realistic_image/enterprise",
    "realistic_image/motion_blur"],
                                                AIModelId = recraftModelId,
                                                CreatedAt = DateTimeOffset.UtcNow,
                                                CreatedByUserId = user.Id.ToString(),
                                        }
                                ],
                                CreatedAt = DateTimeOffset.UtcNow,
                                CreatedByUserId = user.Id.ToString(),
                        };

                        var lumaPhotonModelId = Guid.CreateVersion7();

                        var lumaPhotonModel = new AIModel
                        {
                                Id = lumaPhotonModelId,
                                Name = "Luma Photon",
                                Description = "High-quality image generation model optimized for creative professional workflows and ultra-high fidelity output.",
                                ModelUrl = "luma-ai/photon",
                                Parameters = [
                                        new AIModelParameter
                                {
                                        Id = Guid.CreateVersion7(),
                                        Name = "aspect_ratio",
                                        DisplayName = "Aspect Ratio",
                                        Type = AIModelParameterType.String,
                                        IsRequired = false,
                                        DefaultValue = "16:9",
                                        PossibleValues = ["1:1", "3:4", "4:3", "9:16", "16:9", "9:21", "21:9"],
                                        AIModelId = lumaPhotonModelId,
                                        CreatedAt = DateTimeOffset.UtcNow,
                                        CreatedByUserId = user.Id.ToString(),
                                },
                                new AIModelParameter
                                {
                                        Id = Guid.CreateVersion7(),
                                        Name = "style_reference_weight",
                                        DisplayName = "Style Reference Weight",
                                        Type = AIModelParameterType.Float,
                                        IsRequired = false,
                                        DefaultValue = "0.85",
                                        PossibleValues = [],
                                        AIModelId = lumaPhotonModelId,
                                        CreatedAt = DateTimeOffset.UtcNow,
                                        CreatedByUserId = user.Id.ToString(),
                                }
                                ],
                                CreatedAt = DateTimeOffset.UtcNow,
                                CreatedByUserId = user.Id.ToString(),
                        };



                        await dbContext.AIModels.AddAsync(recraftModel, cts.Token);
                        await dbContext.AIModels.AddAsync(lumaPhotonModel, cts.Token);
                        await dbContext.SaveChangesAsync(cts.Token);
                }



                return app;
        } // TODO()
}
