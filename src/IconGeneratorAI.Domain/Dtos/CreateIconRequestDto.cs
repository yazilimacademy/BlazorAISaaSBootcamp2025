namespace IconGeneratorAI.Domain.Dtos;

public sealed record CreateIconRequestDto
{
    public string Prompt { get; set; }
    public string Size { get; set; }

    public CreateIconRequestDto(string prompt, string size)
    {
        Prompt = prompt;
        Size = size;
    }

    public CreateIconRequestDto()
    {

    }
}
