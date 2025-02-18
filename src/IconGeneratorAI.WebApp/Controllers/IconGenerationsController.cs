using System.Text.Json;
using IconGeneratorAI.Persistence.EntityFramework.Contexts;
using IconGeneratorAI.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IconGeneratorAI.WebApp.Controllers
{
    [Route("api/icon-generations")]
    [ApiController]
    [Authorize]
    public class IconGenerationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public IconGenerationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateIconGenerationRequestDto requestDto, CancellationToken cancellationToken)
        {
            Console.WriteLine(JsonSerializer.Serialize(requestDto));

            return Ok();
        }
    }
}
