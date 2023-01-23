using Microsoft.AspNetCore.Mvc;
using System.Reactive.Linq;
using System.Text.Json;
using UltraPlayBettingSystemExercise.Services.Interfaces;
using UltraPlayBettingSystemExercise.ViewModels;

namespace UltraPlayBettingSystemExercise.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AllMatchesController : ControllerBase
    {
        private readonly IMatchesService service;

        public AllMatchesController(IMatchesService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await service.GetAllMatchesInLast24Hours<MatchViewModel>();

            var jsonResponse = new JsonResult(result, new JsonSerializerOptions() { WriteIndented = true });

            return jsonResponse;
        }
    }
}
