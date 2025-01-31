using System;
using Microsoft.EntityFrameworkCore;

namespace IconGeneratorAI.Persistence.EntityFramework.Extensions;

public static class DbFunctionsExtensions
{
    /// <summary>
    /// Checks if the JSONB array contains the specified value.
    /// </summary>
    /// <param name="dbFunctions">The DbFunctions instance.</param>
    /// <param name="jsonbColumn">The JSONB column.</param>
    /// <param name="value">The value to check for.</param>
    /// <returns>True if the value is contained in the JSONB array; otherwise, false.</returns>
    [DbFunction("jsonb_array_contains", "public")]
    public static bool JsonbArrayContains(this DbFunctions dbFunctions, string jsonbColumn, string value)
    {
        throw new NotSupportedException("This function is for use with Entity Framework Core LINQ queries only.");
    }

    /// <summary>
    /// Checks if the JSONB array contains any of the specified values.
    /// </summary>
    /// <param name="dbFunctions">The DbFunctions instance.</param>
    /// <param name="jsonbColumn">The JSONB column.</param>
    /// <param name="values">The values to check for.</param>
    /// <returns>True if any of the values are contained in the JSONB array; otherwise, false.</returns>
    [DbFunction("jsonb_array_contains_any", "public")]
    public static bool JsonbArrayContainsAny(this DbFunctions dbFunctions, string jsonbColumn, string[] values)
    {
        throw new NotSupportedException("This function is for use with Entity Framework Core LINQ queries only.");
    }
}