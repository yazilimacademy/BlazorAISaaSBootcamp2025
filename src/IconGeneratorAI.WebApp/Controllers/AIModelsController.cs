using IconGeneratorAI.Persistence.EntityFramework.Contexts;
using IconGeneratorAI.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IconGeneratorAI.WebApp.Controllers
{
    [Route("api/ai-models")]
    [ApiController]
    public class AIModelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AIModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var aiModels = await _context
            .AIModels
            .AsNoTracking()
            .Select(x => new GetAllAIModelsDto(x.Id, x.Name, x.Description, x.ModelUrl, x.Parameters.Select(p => p.Name).ToList()))
            .ToListAsync(cancellationToken);

            return Ok(aiModels);
        }

    }
}
