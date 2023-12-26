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
        private readonly IPaymentRepository _paymentRepository;



        public CustomAircraftController(IAircraftRepository aircraftRepository, ILogger<HomeController> logger, IPaymentRepository paymentRepository)
        {
            _aircraftRepository = aircraftRepository;
            _logger = logger;
            _paymentRepository = paymentRepository;
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
        public async Task<IActionResult> Payment(Aircraft model, IFormFile? uploadedImage)
        {
            // Process the form data here (model contains the submitted values)
            // For example:
            // Save the Aircraft details to the database or perform any other operations

            if(uploadedImage != null)
            {
                string imagePath = await _aircraftRepository.UploadImage(uploadedImage);

                // Set the ImageUrl property of the aircraft
                model.ImageUrl = imagePath;
            }


            // Redirect to a success page or return a success message
            return View("~/Views/CustomAircraft/Payment.cshtml", model);
            //return RedirectToAction("Payment", model);

        }



        // GET: Action for Payment Success
        public IActionResult PaymentSuccess()
        {
            return View();
        }

        public async Task<IActionResult> AddPayment(Payment payment, IFormFile uploadedImage)
        {
            try
            {
                if (payment == null)
                {
                    _logger.LogWarning("Invalid request data.");
                    return BadRequest();
                }

                int userId = HttpContext.Session.GetInt32("UserId") ?? 0; // Retrieve from session

                payment.UserId = userId;

                var createPayment = await _paymentRepository.CreatePayment(payment);
                _logger.LogInformation($"Added new payment with ID {createPayment.Id}.");

                if (createPayment == null)
                {
                    return NotFound();
                }

                var aircrafts = await _aircraftRepository.GetAllAircraftsAsync();

                //return CreatedAtAction(nameof(GetPaymentById), new { id = createPayment.Id }, createPayment);
                return View("~/Views/Home/Index.cshtml", aircrafts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a payment.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

    }
}
