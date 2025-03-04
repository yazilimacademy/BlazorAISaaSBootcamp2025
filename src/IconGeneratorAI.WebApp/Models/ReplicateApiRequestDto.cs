namespace IconGeneratorAI.WebApp.Models;

public sealed record ReplicateApiRequestDto
{
    public string ModelUrl { get; set; }
    public Dictionary<string, object> Input { get; set; }

    public ReplicateApiRequestDto(string modelUrl, Dictionary<string, object> input)
    {
        ModelUrl = modelUrl;
        Input = input;
    }

    public ReplicateApiRequestDto()
    {

    }
}
