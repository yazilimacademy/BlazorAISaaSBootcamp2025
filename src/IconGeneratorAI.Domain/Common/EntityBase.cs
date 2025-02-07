namespace IconGeneratorAI.Domain.Common;

public abstract class EntityBase : ICreatedByEntity, IUpdatedByEntity
{
    public virtual Guid Id { get; set; }

    public virtual DateTimeOffset CreatedAt { get; set; }
    public virtual string CreatedByUserId { get; set; }

    public virtual DateTimeOffset? UpdatedAt { get; set; }
    public virtual string? UpdatedByUserId { get; set; }
}

