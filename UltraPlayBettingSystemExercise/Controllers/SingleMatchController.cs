using Microsoft.AspNetCore.Mvc;
using UltraPlayBettingSystemExercise.Services.Interfaces;
using UltraPlayBettingSystemExercise.ViewModels;

namespace UltraPlayBettingSystemExercise.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SingleMatchController : ControllerBase
    {
        private readonly IMatchesService matchesService;

        public SingleMatchController(IMatchesService matchesService)
        {
            this.matchesService = matchesService;
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            if(id == 0)
                return new JsonResult("Id must be valid integer and bigger than 0");

            var res = await matchesService.GetSingleMatchByID<MatchViewModel>(id);

            if (res == null)
                return new JsonResult($"Oops Match with id: {id} could not be found");

            return new JsonResult(res);
        }
    }
}
