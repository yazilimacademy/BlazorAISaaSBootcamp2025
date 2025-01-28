using System;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using IconGeneratorAI.Domain.Settings;
using IconGeneratorAI.WebApp.Models;
using Microsoft.Extensions.Options;

namespace IconGeneratorAI.WebApp.Services;

public sealed class AzureBlobStorageManager : IObjectStorageService
{
    private readonly AzureBlobStorageSettings _settings;
    private readonly BlobServiceClient _blobServiceClient;

    public AzureBlobStorageManager(IOptions<AzureBlobStorageSettings> settings)
    {
        _settings = settings.Value;

        _blobServiceClient = new BlobServiceClient(_settings.ConnectionString);
    }

    public async Task<string> UploadAsync(ObjectStorageUploadRequestDto requestDto, CancellationToken cancellationToken)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(_settings.IconsContainerName);

        var fileExtension = Path.GetExtension(requestDto.FileName);

        var fileName = $"{Guid.CreateVersion7()}{fileExtension}";

        var blobClient = containerClient.GetBlobClient(fileName);

        var uploadOptions = new BlobUploadOptions
        {
            HttpHeaders = new BlobHttpHeaders
            {
                ContentType = requestDto.ContentType, // pdf, png, svg, etc.
                CacheControl = "public, max-age=315360000"
            },
            Metadata = new Dictionary<string, string> {
                { "fileName", requestDto.FileName },
                { "userId", "1234567890" }
            }
        };

        await blobClient.UploadAsync(requestDto.File, uploadOptions, cancellationToken);

        return blobClient.Uri.ToString();
    }
}
