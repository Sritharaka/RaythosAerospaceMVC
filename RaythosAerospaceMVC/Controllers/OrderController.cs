using Microsoft.AspNetCore.Mvc;
using RaythosAerospaceMVC.Models;
using RaythosAerospaceMVC.Repositories;
using RaythosAerospaceMVC.Repository;
using X.PagedList;

namespace RaythosAerospaceMVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly ILogger<PaymentController> _logger; // Add ILogger

        public OrderController(IOrderRepository orderRepository, IPaymentRepository paymentRepository, ILogger<PaymentController> logger)
        {
            _orderRepository = orderRepository;
            _paymentRepository = paymentRepository;
            _logger = logger;
        }
        public async Task<IActionResult> Order()
        {

            //var orders = await _manufacturingProgressRepository.GetAllAsync();

            return View("~/Views/Order/Order.cshtml");
        }

        public async Task<IActionResult> _DisplayOrderTable(int? page)
        {
            int pageNumber = page ?? 1; // If no page is specified, default to page 1
            int pageSize = 10; // Number of items per page

            int userId = HttpContext.Session.GetInt32("UserId") ?? 0; // Retrieve from session

            var orders = await _orderRepository.GetAllAsync(userId);

            // Return a paged list
            var pagedOrders = orders.ToPagedList(pageNumber, pageSize);

            return PartialView("_DisplayOrderTable", pagedOrders);
        }

        public async Task<IActionResult> Edit(int ManufacturingStatusId)
        {
            var aircraft = await _orderRepository.GetByIdAsync(ManufacturingStatusId);
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
            "BasePrice, AirframeProgress, AvionicsSystemsProgress, EnginesProgress, InteriorProgress, OverallProgress, DeliveryDate, ManufacturingComplete, Telephone, EmailAddress, CardHolderName, ShippingDate, AdditionalDetails, AirportCity, AircraftId, SeatingCapacity"
            )] ManufacturingProgress aircraft)
        {
            //if (id != aircraft.Id)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                var existingAircraft = await _orderRepository.GetByIdAsync(aircraft.ManufacturingStatusId);
                if (existingAircraft == null)
                {
                    return NotFound();
                }

                try
                {



                    // Set the ImageUrl property of the aircraft
                    aircraft.UpdatedDate = DateTime.Now;

                    await _orderRepository.UpdateAsync(aircraft);
                    _logger.LogInformation($"Aircraft with ID {aircraft.ManufacturingStatusId} updated at {DateTime.Now}");
                    return RedirectToAction("Order", "Order"); // Redirect to Home/Index upon successful login

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
