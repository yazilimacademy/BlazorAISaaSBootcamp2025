namespace IconGeneratorAI.Shared.Enums
{
    public enum AIModelParameterType
    {
        String = 1,
        Integer = 2,
        Boolean = 3,
        Float = 4,
        Enum = 5
    }

    public static class AIModelParameterTypeExtensions
    {
        public static AIModelParameterType FromInt(int value) =>
            value switch
            {
                1 => AIModelParameterType.String,
                2 => AIModelParameterType.Integer,
                3 => AIModelParameterType.Boolean,
                4 => AIModelParameterType.Float,
                5 => AIModelParameterType.Enum,
                _ => throw new ArgumentOutOfRangeException(nameof(value), $"Value {value} is not valid for AIModelParameterType")
            };
    }

}

