using System;
using System.Security.Claims;
using IconGeneratorAI.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using IconGeneratorAI.Persistence.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;

namespace IconGeneratorAI.WebApp.Components.Account;

public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>
{
    private readonly ApplicationDbContext _context;
    public ApplicationUserClaimsPrincipalFactory(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IOptions<IdentityOptions> options, ApplicationDbContext context) : base(userManager, roleManager, options)
    {
        _context = context;
    }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
    {
        var identity = await base.GenerateClaimsAsync(user);

        identity.AddClaim(new Claim(ClaimTypes.GivenName, user.FullName.FirstName));

        identity.AddClaim(new Claim(ClaimTypes.Surname, user.FullName.LastName));

        identity.AddClaim(new Claim("FullName", user.FullName.ToString()));

        return identity;
    }
}
