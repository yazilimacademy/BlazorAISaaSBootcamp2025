namespace IconGeneratorAI.Domain.ValueObjects;

public sealed record FullName
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    private FullName()
    {

    }

    private FullName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public static FullName Create(string firstName, string lastName)
    {
        return new FullName(firstName, lastName);
    }


    public static FullName Create(string fullName)
    {
        var names = fullName.Split(' ');

        return new FullName(names[0], names[1]);
    }

    public override string ToString()
    {
        return $"{FirstName} {LastName}";
    }
}
