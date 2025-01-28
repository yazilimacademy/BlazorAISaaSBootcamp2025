using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IconGeneratorAI.Domain.Identity;



namespace NoobGGApp.Infrastructure.Persistence.EntityFramework.Configurations;



public sealed class ApplicationUserRoleConfiguration : IEntityTypeConfiguration<ApplicationUserRole>

{

    public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
    {

        // Primary key

        builder.HasKey(r => new { r.UserId, r.RoleId });



        // Maps to the AspNetUserRoles table

        builder.ToTable("application_user_roles");

    }

}
