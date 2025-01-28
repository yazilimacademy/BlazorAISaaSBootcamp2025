using System;
using IconGeneratorAI.WebApp.Models;

namespace IconGeneratorAI.WebApp.Services;

public sealed class AWSS3Manager : IObjectStorageService
{
    public Task<string> UploadAsync(ObjectStorageUploadRequestDto requestDto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
