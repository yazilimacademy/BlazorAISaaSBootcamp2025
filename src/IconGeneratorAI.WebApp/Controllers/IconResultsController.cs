using System.Text.Json;
using IconGeneratorAI.Domain.Dtos;
using IconGeneratorAI.Domain.Entities;
using IconGeneratorAI.Persistence.EntityFramework.Contexts;
using IconGeneratorAI.WebApp.Models;
using IconGeneratorAI.WebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IconGeneratorAI.WebApp.Controllers
{
    [Route("api/icon-results")]
    [ApiController]
    public class IconResultsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IObjectStorageService _objectStorageService;

        public IconResultsController(ApplicationDbContext dbContext, IObjectStorageService objectStorageService)
        {
            _dbContext = dbContext;
            _objectStorageService = objectStorageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {

            var aiModel = await _dbContext
            .AIModels
            .AsNoTracking()
            .Where(x => x.Sizes.Contains("128x128"))
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse("00000000-0000-0000-0000-000000000000"), cancellationToken);

            // var iconResults = await _dbContext
            // .IconResults
            //     .AsNoTracking()
            //     .Select(iconResult => new GetAllIconResultsDto(iconResult.Id, iconResult.Title, iconResult.Description, iconResult.Url, iconResult.CreatedAt))
            //     .ToListAsync(cancellationToken);

            // Console.WriteLine(JsonSerializer.Serialize(iconResults));

            // return Ok(iconResults);

            return Ok("test");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateIconRequestDto requestDto, CancellationToken cancellationToken)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:3000/");

            var response = await client.PostAsJsonAsync($"generate", requestDto, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var imageBytes = await response.Content.ReadAsByteArrayAsync();

                var memoryStream = new MemoryStream(imageBytes);

                var uploadRequestDto = new ObjectStorageUploadRequestDto(memoryStream, "image/webp", "icon-result.webp");

                var imageUrl = await _objectStorageService.UploadAsync(uploadRequestDto, cancellationToken);

                var iconResult = IconResult.Create(requestDto.Prompt, requestDto.Size, imageUrl);

                return Ok(iconResult);

            }
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return NotFound("Not found");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    return BadRequest("Bad request");
                }
                else
                {
                    Console.WriteLine("Something went wrong");
                    return StatusCode(500, "Something went wrong");
                }

            }
        }

    }
}
