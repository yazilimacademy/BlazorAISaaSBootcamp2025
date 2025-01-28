namespace IconGeneratorAI.Domain.Settings;

public sealed record AzureBlobStorageSettings
{
    public string ConnectionString { get; set; }
    public string IconsContainerName { get; set; }
}
