using System;
using IconGeneratorAI.WebApp.Models;

namespace IconGeneratorAI.WebApp.Services;

public interface IObjectStorageService
{
    Task<string> UploadAsync(ObjectStorageUploadRequestDto requestDto, CancellationToken cancellationToken);
}
