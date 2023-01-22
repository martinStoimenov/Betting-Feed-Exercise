using Microsoft.AspNetCore.Mvc;
using System.Reactive.Linq;
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

            //GetDataEveryMinute();

            return new JsonResult(result);
        }

        private void GetDataEveryMinute()
        {
            IObservable<IActionResult> timer = Observable.Generate(
                new { now = DateTimeOffset.Now, count = 0 },
                t => true,
                t => new { t.now, count = t.count + 1 },
                t => t.count,
                t => t.now.AddMinutes(t.count))
            .SelectMany(x => Observable.FromAsync(() => this.Get()));

            timer.Subscribe();
        }
    }
}
