using Microsoft.AspNetCore.Mvc;
using RaythosAerospaceMVC.Models;
using RaythosAerospaceMVC.Repository;
using X.PagedList;

namespace RaythosAerospaceMVC.Controllers
{
    public class AircraftController : Controller
    {
        private readonly IAircraftRepository _aircraftRepository;
        private readonly ILogger<AircraftController> _logger;

        public AircraftController(IAircraftRepository aircraftRepository, ILogger<AircraftController> logger)
        {
            _aircraftRepository = aircraftRepository;
            _logger = logger;
        }



        //GET: Aircrafts
        public async Task<IActionResult> Aircraft()
        {
            //var aircrafts = await _aircraftRepository.GetAllAircraftsAsync();
            //return View(aircrafts);

            return View();

        }


        //public async Task<IActionResult> _DisplayAircraftTable()
        //{
        //    var aircrafts = await _aircraftRepository.GetAllAircraftsAsync();
        //    return PartialView("_DisplayAircraftTable", aircrafts);
        //}

        public async Task<IActionResult> _DisplayAircraftTable(int? page)
        {
            int pageNumber = page ?? 1; // If no page is specified, default to page 1
            int pageSize = 10; // Number of items per page

            var aircrafts = await _aircraftRepository.GetAllAircraftsAsync();

            // Return a paged list
            var pagedAircrafts = aircrafts.ToPagedList(pageNumber, pageSize);

            return PartialView("_DisplayAircraftTable", pagedAircrafts);
        }




        // GET: Aircrafts/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var aircraft = await _aircraftRepository.GetAircraftByIdAsync(id);
            if (aircraft == null)
            {
                return NotFound();
            }

            var pagedAircraftList = new List<Aircraft> { aircraft }.ToPagedList(pageNumber: 1, pageSize: 1);/* _DisplayAircraftTable Aircraft*/

            return View("_DisplayAircraftTable", pagedAircraftList); 
        }


        // GET: Aircrafts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Aircrafts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ModelName, Manufacturer, Description, " +
            "SeatingType, InteriorDesign, AdditionalFeatures, ImageUrl, BasePrice, EngineType, MaximumSpeed, SeatingCapacity, FuelCapacity, Weight," +
            " CreatedDate")] Aircraft aircraft, IFormFile uploadedImage)
        {
            if (ModelState.IsValid)
            {
                string imagePath = await _aircraftRepository.UploadImage(uploadedImage);

                // Set the ImageUrl property of the aircraft
                aircraft.ImageUrl = imagePath;
                aircraft.CreatedDate = DateTime.Now;

                await _aircraftRepository.AddAircraftAsync(aircraft);
                _logger.LogInformation($"Aircraft with ID {aircraft.Id} created at {DateTime.Now}");
                return RedirectToAction(nameof(Aircraft));
            }
            return View(aircraft);
        }

        // GET: Aircrafts/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var aircraft = await _aircraftRepository.GetAircraftByIdAsync(id);
            if (aircraft == null)
            {
                return NotFound();
            }
            return View(aircraft);
        }

        // POST: Aircrafts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( [Bind("Id, ModelName, Manufacturer, Description, " +
            "SeatingType, InteriorDesign, AdditionalFeatures, ImageUrl, BasePrice, EngineType, MaximumSpeed, SeatingCapacity, FuelCapacity, Weight," +
            " CreatedDate")] Aircraft aircraft,  IFormFile uploadedImage)
        {
            //if (id != aircraft.Id)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                var existingAircraft = await _aircraftRepository.GetAircraftByIdAsync(aircraft.Id);
                if (existingAircraft == null)
                {
                    return NotFound();
                }

                try
                {

                    string imagePath = await _aircraftRepository.UploadImage(uploadedImage);

                    // Set the ImageUrl property of the aircraft
                    aircraft.ImageUrl = imagePath;
                    aircraft.UpdatedDate = DateTime.Now;

                    await _aircraftRepository.UpdateAircraftAsync(aircraft);
                    _logger.LogInformation($"Aircraft with ID {aircraft.Id} updated at {DateTime.Now}");
                    return RedirectToAction("Aircraft", "Aircraft"); // Redirect to Home/Index upon successful login

                    //return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    return BadRequest(); // Or handle the exception accordingly
                }
            }
            return View(aircraft);
        }

        // GET: Aircrafts/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var aircraft = await _aircraftRepository.GetAircraftByIdAsync(id);
            if (aircraft == null)
            {
                return NotFound();
            }

            return View(aircraft);
        }

        // POST: Aircrafts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _aircraftRepository.DeleteAircraftAsync(id);
            _logger.LogInformation($"Aircraft with ID {id} deleted at {DateTime.Now}");
            return RedirectToAction("Aircraft", "Aircraft");
        }
    }
}
