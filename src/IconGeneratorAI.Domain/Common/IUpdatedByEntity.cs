namespace IconGeneratorAI.Domain.Common;

public interface IUpdatedByEntity
{
    DateTimeOffset? UpdatedAt { get; set; }
    string? UpdatedByUserId { get; set; }
}
