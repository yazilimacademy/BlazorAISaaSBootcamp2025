namespace IconGeneratorAI.WebApp.Models;

public sealed record ObjectStorageUploadRequestDto
{
    public Stream File { get; set; }
    public string ContentType { get; set; }
    public string FileName { get; set; }

    public ObjectStorageUploadRequestDto(Stream file, string contentType, string fileName)
    {
        File = file;
        ContentType = contentType;
        FileName = fileName;
    }

    public ObjectStorageUploadRequestDto()
    {

    }
}
