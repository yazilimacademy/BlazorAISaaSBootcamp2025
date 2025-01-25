using System;

namespace IconGeneratorAI.Domain.Common;

public interface ICreatedByEntity
{
    DateTimeOffset CreatedAt { get; set; }
    string CreatedByUserId { get; set; }
}
