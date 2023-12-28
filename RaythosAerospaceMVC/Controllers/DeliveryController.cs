using Microsoft.AspNetCore.Mvc;
using RaythosAerospaceMVC.Models;
using RaythosAerospaceMVC.Repositories;
using RaythosAerospaceMVC.Repository;
using X.PagedList;

namespace RaythosAerospaceMVC.Controllers
{
    public class DeliveryController : Controller
    {
        private readonly IDeliveryRepository _deliveryRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly ILogger<PaymentController> _logger; // Add ILogger

        public DeliveryController(IDeliveryRepository deliveryRepository, IPaymentRepository paymentRepository, ILogger<PaymentController> logger)
        {
            _deliveryRepository = deliveryRepository;
            _paymentRepository = paymentRepository;
            _logger = logger;
        }
        public async Task<IActionResult> Delivery()
        {
            return View("~/Views/Delivery/Delivery.cshtml");
        }

        public async Task<IActionResult> _DisplayDeliveryTable(int? page)
        {
            int pageNumber = page ?? 1; // If no page is specified, default to page 1
            int pageSize = 10; // Number of items per page

            int userId = HttpContext.Session.GetInt32("UserId") ?? 0; // Retrieve from session

            var deliveries = await _deliveryRepository.GetAllAsync();

            // Return a paged list
            var pagedDeliveries = deliveries.ToPagedList(pageNumber, pageSize);

            return PartialView("_DisplayDeliveryTable", pagedDeliveries);
        }

        public async Task<IActionResult> Edit(int DeliveryId)
        {
            var delivery = await _deliveryRepository.GetByIdAsync(DeliveryId);
            if (delivery == null)
            {
                return NotFound();
            }
            return View(delivery);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("DeliveryId, ManufacturingStatusId, ModelName, Manufacturer, Description, " +
            "BasePrice, AirframeProgress, AvionicsSystemsProgress, EnginesProgress, InteriorProgress, OverallProgress, DeliveryDate, ManufacturingComplete, Telephone, EmailAddress, CardHolderName, ShippingDate, AdditionalDetails, AirportCity, DeliveryComplete, DeliveryCompleteDate, DeliveryCompleteDescription, WarrentyYears, AircraftId"
            )] Delivery delivery)
        {
            if (ModelState.IsValid)
            {
                var existingDelivery = await _deliveryRepository.GetByIdAsync(delivery.DeliveryId);
                if (existingDelivery == null)
                {
                    return NotFound();
                }

                try
                {
                    delivery.UpdatedDate = DateTime.Now;

                    await _deliveryRepository.UpdateAsync(delivery);
                    _logger.LogInformation($"Delivery with ID {delivery.DeliveryId} updated at {DateTime.Now}");
                    return RedirectToAction("Delivery", "Delivery");
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return View(delivery);
        }
    }
}
