// PaymentRepository.cs
using Microsoft.EntityFrameworkCore;
using RaythosAerospaceMVC.Models;
using System.Net.Mail;
using System.Net;
using Stripe;


namespace RaythosAerospaceMVC.Repository
{
    public class PaymentRepository : IPaymentRepository
    {

        private readonly AerospaceDbContext _context;
        private readonly SmtpClient _smtpClient; // You can configure SMTP settings

        private IStripeClient _stripeClient;

        //private readonly IStripeService _stripeService;
        //private readonly StripeClient _stripeClient;


        public PaymentRepository(AerospaceDbContext context, SmtpClient smtpClient)
        {
            _context = context;
            _smtpClient = smtpClient;

            ////_stripeService = stripeService;
            //_stripeClient = new StripeClient(apiKey);
        }

        public async Task<IEnumerable<Payment>> GetAllPayments()
        {
            return await _context.Payments.ToListAsync();
        }

        public async Task<IEnumerable<Payment>> GetPaymentByUserId(int userId)
        {
            return await _context.Payments.Where(payments => payments.UserId == userId).ToListAsync();
        }
        public async Task<Payment> GetPaymentById(int id)
        {
            return await _context.Payments.FindAsync(id);
        }

        public async Task<Payment> CreatePayment(Payment payment)
        {
            try
            {

                // Your code that triggers the exception goes here
                //_context.Payments.Add(payment);
                Payment newPayment = new Payment
                {
                    AircraftId = payment.Id,
                    ModelName = payment.ModelName,
                    Manufacturer = payment.Manufacturer,
                    Description = payment.Description,
                    SeatingType = payment.SeatingType,
                    InteriorDesign = payment.InteriorDesign,
                    AdditionalFeatures = payment.AdditionalFeatures,
                    ImageUrl = payment.ImageUrl,
                    BasePrice = payment.BasePrice,
                    EngineType = payment.EngineType,
                    MaximumSpeed = payment.MaximumSpeed,
                    FuelCapacity = payment.FuelCapacity,
                    SeatingCapacity = payment.SeatingCapacity,
                    Weight = payment.Weight,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = payment.UpdatedDate,
                    DeletedDate = payment.DeletedDate,
                    UserId = payment.UserId,
                    CardNumber = payment.CardNumber,
                    ExpiryDate = payment.ExpiryDate,
                    CVV = payment.CVV,
                    CardHolderName = payment.CardHolderName,
                    Telephone = payment.Telephone,
                    EmailAddress = payment.EmailAddress,
                    uploadedImage = payment.uploadedImage
                };

                //_context.AddOrUpdate(_context, newPayment);
                //_context.Payments.Add(newPayment);
                //await _context.SaveChangesAsync();

                _context.Payments.Add(newPayment);
                await _context.SaveChangesAsync();

                // Get the last inserted row
                var lastPayment = await _context.Payments
                    .OrderByDescending(p => p.Id)
                    .FirstOrDefaultAsync();

                //Update the UserId with the UserId from the payment object
                if (lastPayment != null)
                {
                    ManufacturingProgress Manufacturing = new ManufacturingProgress
                    {
                        PaymentId = lastPayment.Id,
                        AircraftId = payment.Id,
                        ModelName = payment.ModelName,
                        Manufacturer = payment.Manufacturer,
                        Description = payment.Description,
                        SeatingType = payment.SeatingType,
                        InteriorDesign = payment.InteriorDesign,
                        AdditionalFeatures = payment.AdditionalFeatures,
                        ImageUrl = payment.ImageUrl,
                        BasePrice = payment.BasePrice,
                        EngineType = payment.EngineType,
                        MaximumSpeed = payment.MaximumSpeed,
                        FuelCapacity = payment.FuelCapacity,
                        SeatingCapacity = payment.SeatingCapacity,
                        Weight = payment.Weight,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = payment.UpdatedDate,
                        DeletedDate = payment.DeletedDate,
                        UserId = payment.UserId,
                        CardNumber = payment.CardNumber,
                        ExpiryDate = payment.ExpiryDate,
                        CVV = payment.CVV,
                        CardHolderName = payment.CardHolderName,
                        Telephone = payment.Telephone,
                        EmailAddress = payment.EmailAddress,
                        uploadedImage = payment.uploadedImage,
                        DeliveryComplete = "No",
                    };


                    _context.ManufacturingProgresses.Add(Manufacturing);
                    await _context.SaveChangesAsync();
                }

                if (payment.BasePrice > 0)
                {
                    StripeConfiguration.ApiKey = "sk_test_51ODNhkEtM0xTlORRYvpO28VCqjGHUjOMGJUAsHGvJ3VanoUVXtGzfaVZCPhkXSUGJ70tP58Plm6yr9yVUsdKXR8Q00BYz5Knm2";


                    string customerId = "cus_P1SIVF2NchII6L"; // Replace with the actual customer ID
                    string customerName = await GetCustomerName(customerId);
                    // Correct usage might be something like this:
                    //Stripe.PaymentIntentAutomaticPaymentMethodsOptions automaticPaymentMethodsOptions = new Stripe.PaymentIntentAutomaticPaymentMethodsOptions
                    //{
                    //    Enabled = true, // Set the Enabled property according to your requirement
                    //                    // Other properties assignment if needed
                    //};

                    var options = new PaymentIntentCreateOptions
                    {
                        Amount = (long?)payment.BasePrice,
                        Currency = "USD",
                        //AutomaticPaymentMethods = true
                        Customer = customerId,
                        //ReceiptEmail = payment.Email,
                        // Add additional options as needed
                    };

                    var service = new PaymentIntentService();
                    var paymentIntent = await service.CreateAsync(options);

                }



                await SendContactEmail(payment);

                return newPayment;
            }
            catch (StripeException stripeEx)
            {
                // Log or handle the Stripe-specific exception
                Console.WriteLine("Stripe API Error: " + stripeEx.Message);
                // Log stripeEx.StripeError for more detailed information
                throw; // Re-throw the exception or handle it as needed
            }

            catch (DbUpdateException ex)
            {
                // Log the exception details, including inner exceptions
                var fullExceptionMessage = GetFullExceptionMessage(ex);
                //Logger.LogError(fullExceptionMessage);

                // Rethrow the exception if needed
                throw;
            }
        }

        private async Task SendContactEmail(Payment payment)
        {
            try
            {
                string smtpServer = "smtp.gmail.com";
                string smtpUsername = "keminda4lk@gmail.com";
                string smtpPassword = "jvdsgagljwyomquz";

                using (var mailMessage = new MailMessage())
                using (var smtpClient = new SmtpClient(smtpServer))
                {
                    mailMessage.From = new MailAddress("keminda4lk@gmail.com");
                    mailMessage.To.Add("keminda4lk@gmail.com");
                    mailMessage.Subject = "Payment";
                    mailMessage.Body = $"Name: {payment.CardHolderName}, \nModel Name: {payment.ModelName}, \nManufacturer: {payment.Manufacturer},\nContact Number: {payment.Telephone},\nPrice: {payment.BasePrice} USD, \nPayment Date: {DateTime.Now}";
                    mailMessage.IsBodyHtml = false;

                    smtpClient.Port = 587;
                    smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                    smtpClient.EnableSsl = true;

                    await smtpClient.SendMailAsync(mailMessage);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, log errors, or send an error response to the user
                Console.WriteLine(ex.ToString());
                // You might want to log the error using a proper logging library
                // and consider returning an error response to the user if applicable
            }
        }


        public async Task<string> GetCustomerName(string customerId)
        {
            var customerService = new CustomerService(_stripeClient);

            try
            {
                var customer = await customerService.GetAsync(customerId);
                return customer.Name; // Assuming the customer has a 'Name' property in Stripe
            }
            catch (StripeException e)
            {
                // Handle Stripe API exceptions (e.g., invalid customer ID)
                // Log or handle the exception as needed
                throw; // Re-throw the exception or return a default value/error message
            }
        }

        private string GetFullExceptionMessage(Exception exception)
        {
            var fullMessage = exception.Message;

            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
                fullMessage += " -> " + exception.Message;
            }

            return fullMessage;
        }

        public async Task<Payment> UpdatePayment(Payment payment)
        {
            _context.Entry(payment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return payment;
        }

        public async Task DeletePayment(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                await _context.SaveChangesAsync();
            }
        }

        // Implement other methods for payment-related operations
    }
}
