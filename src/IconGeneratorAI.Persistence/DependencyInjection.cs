using System;
using IconGeneratorAI.Persistence.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IconGeneratorAI.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql("IconGeneratorAI")
        .UseUpperSnakeCaseNamingConvention()
        );

        // Transactions
        // transactions
        // UserTransaction
        // user_transactions
        return services;
    }
}
