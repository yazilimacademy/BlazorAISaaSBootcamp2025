using IconGeneratorAI.Domain.Entities;
using IconGeneratorAI.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IconGeneratorAI.Persistence.EntityFramework.Contexts;

public sealed class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>
{
    public DbSet<UserBalance> UserBalances { get; set; }
    public DbSet<UserBalanceTransaction> UserBalanceTransactions { get; set; }
    public DbSet<AIModel> AIModels { get; set; }
    public DbSet<AIModelParameter> AIModelParameters { get; set; }
    public DbSet<IconGeneration> IconGenerations { get; set; }
    public DbSet<IconGenerationParameter> IconGenerationParameters { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
