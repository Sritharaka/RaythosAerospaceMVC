using Microsoft.AspNetCore.Mvc;
using RaythosAerospaceMVC.Models;
using RaythosAerospaceMVC.Repository;
using System.Diagnostics;

namespace RaythosAerospaceMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAircraftRepository _aircraftRepository;


        public HomeController(IAircraftRepository aircraftRepository, ILogger<HomeController> logger)
        {
            _aircraftRepository = aircraftRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {

            var aircrafts = await _aircraftRepository.GetAllAircraftsAsync();

            return View(aircrafts);
        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}