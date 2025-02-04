using IconGeneratorAI.Domain.Common;
using IconGeneratorAI.Domain.Identity;

namespace IconGeneratorAI.Domain.Entities;

public sealed class UserBalance : EntityBase
{
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; }

    public int Balance { get; set; }

    public ICollection<UserBalanceTransaction> Transactions { get; set; }

    public static UserBalance Create(Guid userId, int balance)
    {
        return new UserBalance
        {
            Id = Guid.CreateVersion7(),
            UserId = userId,
            Balance = balance,
            CreatedAt = DateTimeOffset.UtcNow,
            CreatedByUserId = userId.ToString(),
        };

    }
}