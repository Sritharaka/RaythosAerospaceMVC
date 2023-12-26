using Microsoft.AspNetCore.Mvc;
using RaythosAerospaceMVC.Models;
using RaythosAerospaceMVC.Repository;

namespace RaythosAerospaceMVC.Controllers
{
    public class PaymentController : Controller
    {

        private readonly IOtpRepository _otpRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly ILogger<PaymentController> _logger; // Add ILogger

        public PaymentController(IOtpRepository otpRepository, IPaymentRepository paymentRepository, ILogger<PaymentController> logger)
        {
            _otpRepository = otpRepository;
            _paymentRepository = paymentRepository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SendOtp(Otps request)
        {

            // Send the OTP via InfoBip SMS
            bool sent = await _otpRepository.SendOtpAsync(request.Telephone, request.Otp);

            if (!sent)
            {

                int userId = HttpContext.Session.GetInt32("UserId") ?? 0; // Retrieve from session

                request.UserId = userId;
                request.CreateDate = DateTime.Now;
                // Save the OTP in the database
                var saved = await _otpRepository.SaveOtpAsync(request);

                if (saved != null)
                {
                    return View("~/Views/CustomAircraft/Payment.cshtml");
                }
                else
                {
                    return BadRequest(new { message = "Failed to save OTP." });
                }
            }
            else
            {
                return BadRequest(new { message = "Failed to send OTP." });
            }
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

                var createPayment = await _paymentRepository.CreatePayment(payment);
                _logger.LogInformation($"Added new payment with ID {createPayment.Id}.");
                //return CreatedAtAction(nameof(GetPaymentById), new { id = createPayment.Id }, createPayment);
                return View("~/Views/CustomAircraft/Payment.cshtml", createPayment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a payment.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
