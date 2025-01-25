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
}
