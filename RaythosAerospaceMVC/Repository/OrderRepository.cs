using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using RaythosAerospaceMVC.Models;
using System.Net.Mail;
using System.Net;
using Stripe.Climate;

namespace RaythosAerospaceMVC.Repositories
{
    public class OrderRepository : IOrderRepository // Renamed class to OrderRepository
    {
        private readonly AerospaceDbContext _context;

        public OrderRepository(AerospaceDbContext context)
        {
            _context = context;
        }

        public async Task<List<ManufacturingProgress>> GetAllAsync(int userId)
        {
            return await _context.ManufacturingProgresses.Where(Order => Order.UserId == userId).ToListAsync();
        }

        public async Task<ManufacturingProgress> GetByIdAsync(int id)
        {
            return await _context.ManufacturingProgresses.FindAsync(id);
        }

        public async Task AddAsync(ManufacturingProgress progress)
        {
            _context.ManufacturingProgresses.Add(progress);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ManufacturingProgress progress)
        {
            var existingAircraft = await _context.ManufacturingProgresses.FindAsync(progress.ManufacturingStatusId);

            if (existingAircraft == null)
            {
                throw new InvalidOperationException("Aircraft not found");
            }
            //var newAircraft = new Aircraft
            //{

            existingAircraft.AirportCity = progress.AirportCity;
            existingAircraft.ShippingDate = progress.ShippingDate;
            existingAircraft.AdditionalDetails = progress.AdditionalDetails;
            existingAircraft.ShippingComplete = true;
            existingAircraft.UpdatedDate = progress.UpdatedDate;



            // Copy other properties as needed
            //};

            // Update the properties of the existingAircraft entity with the new values
            //_context.Entry(existingAircraft).CurrentValues.SetValues(newAircraft);

            await _context.SaveChangesAsync();

            var existingDelivery = await _context.Delivery.FindAsync(progress.ManufacturingStatusId);


            if (existingAircraft != null && existingDelivery == null)
            {
                Delivery NewDelivery = new Delivery
                {

                    AirportCity = progress.AirportCity,
                    ShippingDate = progress.ShippingDate,
                    AdditionalDetails = progress.AdditionalDetails,
                    ShippingComplete = true,
                    CreatedDate = DateTime.Now,
                    Manufacturer = progress.Manufacturer,
                    BasePrice = progress.BasePrice,
                    AirframeProgress = progress.AirframeProgress,
                    AvionicsSystemsProgress = progress.AvionicsSystemsProgress,
                    EnginesProgress = progress.EnginesProgress,
                    InteriorProgress = progress.InteriorProgress,
                    OverallProgress = progress.OverallProgress,
                    DeliveryDate = progress.DeliveryDate,
                    ManufacturingComplete = progress.ManufacturingComplete,
                    Telephone = progress.Telephone,
                    EmailAddress = progress.EmailAddress,
                    ManufacturingStatusId = progress.ManufacturingStatusId,
                    CardHolderName = progress.CardHolderName,
                    ModelName = progress.ModelName,
                    AircraftId = progress.AircraftId,
                    SeatingCapacity = progress.SeatingCapacity,


                };


                _context.Delivery.Add(NewDelivery);
                await _context.SaveChangesAsync();
            }
            else
            {

                existingDelivery.AirportCity = progress.AirportCity;
                existingDelivery.ShippingDate = progress.ShippingDate;
                existingDelivery.AdditionalDetails = progress.AdditionalDetails;
                existingDelivery.ShippingComplete = true;
                existingDelivery.UpdatedDate = progress.UpdatedDate;
                existingDelivery.ModelName = progress.ModelName;
                existingDelivery.AirframeProgress = progress.AirframeProgress;
                existingDelivery.AvionicsSystemsProgress = progress.AvionicsSystemsProgress;
                existingDelivery.EnginesProgress = progress.EnginesProgress;
                existingDelivery.InteriorProgress = progress.InteriorProgress;
                existingDelivery.OverallProgress = progress.OverallProgress;
                existingDelivery.DeliveryDate = progress.DeliveryDate;
                existingDelivery.ManufacturingComplete = progress.ManufacturingComplete;
                existingDelivery.AircraftId = progress.AircraftId;
                existingDelivery.SeatingCapacity = progress.SeatingCapacity;



                await _context.SaveChangesAsync();

            }


            SendContactEmail(progress);
        }

        private async Task SendContactEmail(ManufacturingProgress ManufacturingProgress)
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
                    mailMessage.Subject = "Shipping Details";
                    mailMessage.Body = $"Owner Name: {ManufacturingProgress.CardHolderName}, \nModel Name: {ManufacturingProgress.ModelName}, \nManufacturer: {ManufacturingProgress.Manufacturer},\nContact Number: {ManufacturingProgress.Telephone},\nPrice: {ManufacturingProgress.BasePrice} USD, \nAirframe Progress: {ManufacturingProgress.AirframeProgress}, \nEngines Progress: {ManufacturingProgress.EnginesProgress}, \nInterior Progress: {ManufacturingProgress.InteriorProgress}, \nAvionics Systems Progress: {ManufacturingProgress.AvionicsSystemsProgress}, \nOverall Progress: {ManufacturingProgress.OverallProgress}, \nDelivery Date: {ManufacturingProgress.DeliveryDate}, \nEmail Date: {DateTime.Now}, \nAirport City : {ManufacturingProgress.AirportCity}, \nShipping Date : {ManufacturingProgress.ShippingDate}";
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

        public async Task DeleteAsync(ManufacturingProgress progress)
        {
            _context.ManufacturingProgresses.Remove(progress);
            await _context.SaveChangesAsync();
        }

    }
}
