using IconGeneratorAI.Domain.Enums;
using IconGeneratorAI.Persistence.EntityFramework.Contexts;
using IconGeneratorAI.Shared.Enums;
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
            .Include(x => x.Parameters)
            .Select(x => new GetAllAIModelsDto(x.Id, x.Name, x.Parameters.Select(p => new GetAllAIModelsAIParameterDto(p.Id, p.AIModelId, p.DisplayName, AIModelParameterTypeExtensions.FromInt((int)p.Type), p.IsRequired, p.DefaultValue, p.PossibleValues)).ToList()))
            .ToListAsync(cancellationToken);

            return Ok(aiModels);
        }

    }
}
