using System.Security.Claims;
using System.Text.Json;
using IconGeneratorAI.Domain.Entities;
using IconGeneratorAI.Domain.Enums;
using IconGeneratorAI.Persistence.EntityFramework.Contexts;
using IconGeneratorAI.Shared.Models;
using IconGeneratorAI.WebApp.Hubs;
using IconGeneratorAI.WebApp.Models;
using IconGeneratorAI.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace IconGeneratorAI.WebApp.Controllers;

[Route("api/icon-generations")]
[ApiController]
[Authorize]
public class IconGenerationsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IObjectStorageService _objectStorageService;
    private readonly IHubContext<UserHub> _userHub;

    public IconGenerationsController(ApplicationDbContext context, IObjectStorageService objectStorageService, IHubContext<UserHub> userHub)
    {
        _context = context;
        _objectStorageService = objectStorageService;
        _userHub = userHub;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        var iconGenerations = await _context.IconGenerations
            .AsNoTracking()
            .Include(x => x.AIModel)
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new GetIconGenerationsResponseDto
            {
                Id = x.Id,
                Prompt = x.Prompt,
                ImageUrl = x.ImageUrl,
                CreatedAt = x.CreatedAt,
                AIModelName = x.AIModel.Name,
                Style = x.Style.ToString()
            })
            .ToListAsync(cancellationToken);

        return Ok(iconGenerations);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateIconGenerationRequestDto requestDto, CancellationToken cancellationToken)
    {

        var aiModel = await _context
        .AIModels
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.Id == requestDto.AIModelId, cancellationToken);

        var parameterIds = requestDto.Parameters.Select(x => x.Id);

        var parameters = await _context
        .AIModelParameters
        .AsNoTracking()
        .Where(x => parameterIds.Contains(x.Id)) // 11, 15, 101
        .ToListAsync(cancellationToken);

        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        var modelUrl = aiModel.ModelUrl;

        var input = new Dictionary<string, object>();

        List<IconGenerationParameter> iconGenerationParameters = [];

        var userBalance = await _context
     .UserBalances
     .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

        foreach (var parameter in parameters)
        {
            var parameterValue = requestDto.Parameters.FirstOrDefault(x => x.Id == parameter.Id).Value;

            if (parameter.Type == AIModelParameterType.Integer)
            {
                input[parameter.Name] = int.Parse(parameterValue);
            }
            else if (parameter.Type == AIModelParameterType.Float)
            {
                input[parameter.Name] = float.Parse(parameterValue);
            }
            else if (parameter.Type == AIModelParameterType.Boolean)
            {
                input[parameter.Name] = bool.Parse(parameterValue);
            }
            else
            {
                input[parameter.Name] = parameterValue;
            }

            var iconGenerationParameter = IconGenerationParameter.Create(parameter.Id, parameterValue, userId);

            iconGenerationParameters.Add(iconGenerationParameter);

        }

        input["prompt"] = requestDto.Prompt;

        var replicateApiRequestDto = new ReplicateApiRequestDto(modelUrl, input);

        using var client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:3001/");

        // 2. İkon oluşturma isteğini yap
        var response = await client.PostAsJsonAsync($"generate", replicateApiRequestDto, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            var imageBytes = await response.Content.ReadAsByteArrayAsync();
            var memoryStream = new MemoryStream(imageBytes);
            var uploadRequestDto = new ObjectStorageUploadRequestDto(memoryStream, "image/webp", "icon-generation.webp"); // Dosya adını güncelledim

            var imageUrl = await _objectStorageService.UploadAsync(uploadRequestDto, cancellationToken);

            var iconGeneration = IconGeneration.Create(userId, aiModel.Id, requestDto.Prompt, imageUrl);

            iconGeneration.Parameters = iconGenerationParameters;

            _context.IconGenerations.Add(iconGeneration);

            DecreaseUserBalance(userBalance, userId);

            await _context.SaveChangesAsync(cancellationToken);

            var responseDto = new CreateIconGenerationResponseDto(iconGeneration.Id, imageUrl);

            await _userHub.Clients.User(userId.ToString()).SendAsync("ReceiveUserBalance", userBalance.Balance);

            return Ok(responseDto);
        }

        if (!response.IsSuccessStatusCode)
        {
            return BadRequest();
        }


        return Ok();
    }


    private void DecreaseUserBalance(UserBalance userBalance, Guid userId)
    {
        //1500
        userBalance.Balance -= 1;
        userBalance.UpdatedAt = DateTimeOffset.UtcNow;
        userBalance.UpdatedByUserId = userId.ToString();

        var userBalanceId = userBalance.Id;
        var transactionType = UserBalanceTransactionType.Remove;
        var amount = 1;
        var balanceAfterTransaction = userBalance.Balance; // 1499
        var description = "Icon generation";
        var createdByUserId = userId;

        var userBalanceTransaction = UserBalanceTransaction.Create(userBalanceId, transactionType, amount, balanceAfterTransaction, description, createdByUserId);

        Console.WriteLine("UserBalanceId: " + userBalanceId);

        Console.WriteLine(JsonSerializer.Serialize(userBalanceTransaction));

        _context.UserBalanceTransactions.Add(userBalanceTransaction);

    }
}
