using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RaythosAerospaceMVC.Models;
using RaythosAerospaceMVC.Repository;
using X.PagedList;

namespace RaythosAerospaceMVC.Controllers
{
    public class CustomAircraftController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAircraftRepository _aircraftRepository;


        public CustomAircraftController(IAircraftRepository aircraftRepository, ILogger<HomeController> logger)
        {
            _aircraftRepository = aircraftRepository;
            _logger = logger;
        }

        // GET: Aircrafts/CustomAircraft/5
        public async Task<IActionResult> CustomAircraft(int id)
        {
            var aircraft = await _aircraftRepository.GetAircraftByIdAsync(id);
            if (aircraft == null)
            {
                return NotFound();
            }


            return View("~/Views/CustomAircraft/CustomAircraft.cshtml", aircraft);
        }

        public IActionResult Index()
        {
            return View();
        }

        //GET: Display the form view
        //public async Task<IActionResult> Payment()
        //{
        //    return View();
        //}

        // POST: Handle form submission
        //[HttpPost]
        public async Task<IActionResult> Payment(Aircraft model)
        {
            // Process the form data here (model contains the submitted values)
            // For example:
            // Save the Aircraft details to the database or perform any other operations

            int userId = HttpContext.Session.GetInt32("UserId") ?? 0; // Retrieve from session


            // Redirect to a success page or return a success message
            return View("~/Views/CustomAircraft/Payment.cshtml", model);
            //return RedirectToAction("Payment", model);

        }



        // GET: Action for Payment Success
        public IActionResult PaymentSuccess()
        {
            return View();
        }

    }
}
