using System.Text.Json;
using IconGeneratorAI.Domain.Dtos;
using IconGeneratorAI.WebApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IconGeneratorAI.WebApp.Controllers
{
    [Route("api/icon-results")]
    [ApiController]
    public class IconResultsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public IconResultsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var iconResults = await _dbContext
            .IconResults
                .AsNoTracking()
                .Select(iconResult => new GetAllIconResultsDto(iconResult.Id, iconResult.Title, iconResult.Description, iconResult.Url, iconResult.CreatedAt))
                .ToListAsync(cancellationToken);

            Console.WriteLine(JsonSerializer.Serialize(iconResults));

            return Ok(iconResults);
        }

    }
}
