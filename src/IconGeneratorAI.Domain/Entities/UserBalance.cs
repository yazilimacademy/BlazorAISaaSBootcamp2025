using IconGeneratorAI.Domain.Common;
using NoobGGApp.Domain.Identity;

namespace IconGeneratorAI.Domain.Entities;

public sealed class UserBalance : EntityBase
{
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; }

    public int Balance { get; set; }

    public ICollection<UserBalanceTransaction> Transactions { get; set; }
}
