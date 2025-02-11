using System.Diagnostics;
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
            .Where(x => x.Parameters.Any(p => p.Name == "aspect_ratio" && p.DefaultValue == "1:1"))
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
            // 1. Stopwatch'ı başlat
            var stopwatch = Stopwatch.StartNew();

            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:3000/");

            // 2. İkon oluşturma isteğini yap
            var response = await client.PostAsJsonAsync($"generate", requestDto, cancellationToken);

            // 3. Stopwatch'ı durdur
            stopwatch.Stop();
            var generationTime = stopwatch.Elapsed; // Geçen süreyi al

            if (response.IsSuccessStatusCode)
            {
                var imageBytes = await response.Content.ReadAsByteArrayAsync();
                var memoryStream = new MemoryStream(imageBytes);
                var uploadRequestDto = new ObjectStorageUploadRequestDto(memoryStream, "image/webp", "icon-generation.webp"); // Dosya adını güncelledim

                var imageUrl = await _objectStorageService.UploadAsync(uploadRequestDto, cancellationToken);

                // **ÖNEMLİ: IconGeneration entity'sini oluştur ve veritabanına kaydet**
                // var iconGeneration = new IconGeneration() // IconGeneration entity'sini new ile oluşturuyoruz
                // {
                //     AIModelId = requestDto.AIModelId, // RequestDto'dan veya nereden geliyorsa AIModelId'yi al
                //     Prompt = requestDto.Prompt,
                //     Style = requestDto.Style, // RequestDto'dan veya nereden geliyorsa Style'ı al
                //     Size = requestDto.Size,
                //     ImageUrl = imageUrl,
                //     UserId = Guid.NewGuid(), // Örnek olarak Guid.NewGuid() atadım, gerçek uygulamada kullanıcı bilgisini almalısınız
                //     PrimaryColor = requestDto.PrimaryColor, // RequestDto'dan veya nereden geliyorsa PrimaryColor'ı al
                //     GenerationTime = generationTime // Hesapladığımız GenerationTime'ı entity'ye ekle
                // };

                // _dbContext.IconGenerations.Add(iconGeneration); // Entity'yi DbContext'e ekle
                // await _dbContext.SaveChangesAsync(cancellationToken); // Değişiklikleri veritabanına kaydet

                // Oluşturulan IconGeneration entity'sini döndür
                // return Ok(iconGeneration); // Başarılı sonuç olarak IconGeneration entity'sini döndürüyoruz
                return Ok();

            }
            else
            {
                // Hata durumunda da GenerationTime'ı loglayabilir veya işleyebilirsiniz.
                Console.WriteLine($"İkon oluşturma başarısız oldu. Status Code: {response.StatusCode}, Generation Time: {generationTime}");

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
