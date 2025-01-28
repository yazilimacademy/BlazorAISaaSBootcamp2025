using IconGeneratorAI.Domain.Common;
using IconGeneratorAI.Domain.Entities;
using IconGeneratorAI.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace IconGeneratorAI.Domain.Identity
{
    public sealed class ApplicationUser : IdentityUser<Guid>, ICreatedByEntity, IUpdatedByEntity
    {
        public FullName FullName { get; set; }
        public UserBalance UserBalance { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedByUserId { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public string? UpdatedByUserId { get; set; }
    }
}
