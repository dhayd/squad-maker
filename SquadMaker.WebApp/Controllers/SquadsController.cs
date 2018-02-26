using Domain;
using Microsoft.AspNetCore.Mvc;
using SquadMaker.WebApp.Models;

namespace SquadMaker.WebApp.Controllers
{
    public class SquadsController : Controller
    {
        private readonly ISquadMaker _squadMaker;

        public SquadsController(ISquadMaker squadMaker)
        {
            _squadMaker = squadMaker;
        }

        public IActionResult Make(SquadMakeRequestViewModel squadMakeRequest)
        {
            var squadSetup = _squadMaker.Make(squadMakeRequest?.NumberOfSquads ?? 0);
            return View(squadSetup);
        }
    }
}