using IconGeneratorAI.Domain.Common;
using IconGeneratorAI.Domain.Enums;

namespace IconGeneratorAI.Domain.Entities;

public sealed class UserBalanceTransaction : EntityBase
{
    public Guid UserBalanceId { get; set; }
    public UserBalance UserBalance { get; set; }

    public UserBalanceTransactionType Type { get; set; } /// userBalanceTransaction.Type == UserBalanceTransactionType.Add
    public int Amount { get; set; }
    public int BalanceAfterTransaction { get; set; }

    public string? Description { get; set; }

    public static UserBalanceTransaction Create(Guid userBalanceId, UserBalanceTransactionType type, int amount, int balanceAfterTransaction, string? description, Guid userId)
    {
        return new UserBalanceTransaction
        {
            Id = Guid.CreateVersion7(),
            UserBalanceId = userBalanceId,
            Type = type,
            Amount = amount,
            BalanceAfterTransaction = balanceAfterTransaction,
            Description = description,
            CreatedAt = DateTimeOffset.UtcNow,
            CreatedByUserId = userId.ToString(),
        };
    }
}
