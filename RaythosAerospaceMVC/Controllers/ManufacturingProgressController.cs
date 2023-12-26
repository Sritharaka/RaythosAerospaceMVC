using Microsoft.AspNetCore.Mvc;
using RaythosAerospaceMVC.Models;
using RaythosAerospaceMVC.Repositories;
using RaythosAerospaceMVC.Repository;
using X.PagedList;

namespace RaythosAerospaceMVC.Controllers
{
    public class ManufacturingProgressController : Controller
    {
        private readonly IManufacturingProgressRepository _manufacturingProgressRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly ILogger<PaymentController> _logger; // Add ILogger

        public ManufacturingProgressController(IManufacturingProgressRepository manufacturingProgressRepository, IPaymentRepository paymentRepository, ILogger<PaymentController> logger)
        {
            _manufacturingProgressRepository = manufacturingProgressRepository;
            _paymentRepository = paymentRepository;
            _logger = logger;
        }
        public async Task<IActionResult> ManufacturingProgress()
        {

            //var orders = await _manufacturingProgressRepository.GetAllAsync();

            return View("~/Views/ManufacturingProgress/ManufacturingProgress.cshtml");
        }

        public async Task<IActionResult> _DisplayManufacturingProgressTable(int? page)
        {
            int pageNumber = page ?? 1; // If no page is specified, default to page 1
            int pageSize = 10; // Number of items per page

            var orders = await _manufacturingProgressRepository.GetAllAsync();

            // Return a paged list
            var pagedOrders = orders.ToPagedList(pageNumber, pageSize);

            return PartialView("_DisplayManufacturingProgressTable", pagedOrders);
        }

        public async Task<IActionResult> Edit(int ManufacturingStatusId)
        {
            var aircraft = await _manufacturingProgressRepository.GetByIdAsync(ManufacturingStatusId);
            if (aircraft == null)
            {
                return NotFound();
            }
            return View(aircraft);
        }

        // POST: Aircrafts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ManufacturingStatusId, ModelName, Manufacturer, Description, " +
            "BasePrice, AirframeProgress, AvionicsSystemsProgress, EnginesProgress, InteriorProgress, OverallProgress, DeliveryDate, ManufacturingComplete, Telephone, EmailAddress, CardHolderName"
            )] ManufacturingProgress aircraft)
        {
            //if (id != aircraft.Id)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                var existingAircraft = await _manufacturingProgressRepository.GetByIdAsync(aircraft.ManufacturingStatusId);
                if (existingAircraft == null)
                {
                    return NotFound();
                }

                try
                {

             

                    // Set the ImageUrl property of the aircraft
                    aircraft.UpdatedDate = DateTime.Now;

                    await _manufacturingProgressRepository.UpdateAsync(aircraft);
                    _logger.LogInformation($"Aircraft with ID {aircraft.ManufacturingStatusId} updated at {DateTime.Now}");
                    return RedirectToAction("ManufacturingProgress", "ManufacturingProgress"); // Redirect to Home/Index upon successful login

                    //return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    return BadRequest(); // Or handle the exception accordingly
                }
            }
            return View(aircraft);
        }

    }
}
