using Microsoft.AspNetCore.Mvc;

namespace UltraPlayBettingSystemExercise.Controllers
{
    [ApiController]
    public class ExceptionHandlerController : ControllerBase
    {
        [Route("/error")]
        public IActionResult HandleError() => new JsonResult("Sorry we was not able to find what you want... :(");
             
    }
}
