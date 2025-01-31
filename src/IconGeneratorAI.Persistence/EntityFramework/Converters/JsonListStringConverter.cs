using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IconGeneratorAI.Persistence.EntityFramework.Converters;

public class JsonListStringConverter : ValueConverter<List<string>, string>
{
    public JsonListStringConverter(ConverterMappingHints? mappingHints = null)
        : base(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
            v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null) ?? new List<string>(),
            mappingHints)
    {
    }
}
