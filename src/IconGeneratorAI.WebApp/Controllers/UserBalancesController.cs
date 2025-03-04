using System.Security.Claims;
using IconGeneratorAI.Persistence.EntityFramework.Contexts;
using IconGeneratorAI.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IconGeneratorAI.WebApp.Controllers
{
    [Route("api/user-balances")]
    [ApiController]
    [Authorize]
    public class UserBalancesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserBalancesController> _logger;

        public UserBalancesController(ApplicationDbContext context, ILogger<UserBalancesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetBalanceAsync(CancellationToken cancellationToken)
        {
            try
            {
                var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var userBalance = await _context.UserBalances
                    .AsNoTracking()
                    .Where(ub => ub.UserId == userId)
                    .Select(ub => ub.Balance)
                    .FirstOrDefaultAsync(cancellationToken);


                return Ok(new UserBalanceWidgetDto(userBalance));
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting balance: {Message}", ex.Message);
                _logger.LogError("Stack trace: {StackTrace}", ex.StackTrace);
                _logger.LogError("Inner exception: {InnerException}", ex.ToString());

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
