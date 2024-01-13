using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RaythosAerospaceMVC.Models;
using System.Net.Mail;
using System.Net;
using Stripe.Climate;
using Microsoft.EntityFrameworkCore;

namespace RaythosAerospaceMVC.Repositories
{
    public class DeliveryRepository : IDeliveryRepository // Renamed class to DeliveryRepository
    {
        private readonly AerospaceDbContext _context;

        public DeliveryRepository(AerospaceDbContext context)
        {
            _context = context;
        }

        public async Task<List<Delivery>> GetAllAsync()
        {
            return await _context.Delivery.ToListAsync();
        }

        public async Task<Delivery> GetByIdAsync(int DeliveryId)
        {
            return await _context.Delivery.FindAsync(DeliveryId);
        }

        public async Task AddAsync(Delivery Delivery)
        {
            _context.Delivery.Add(Delivery);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Delivery progress)
        {
            var existingDelivery = await _context.Delivery.FindAsync(progress.DeliveryId);

            if (existingDelivery != null)
            {
            
                // Update existing Delivery
                existingDelivery.DeliveryCompleteDate = progress.DeliveryCompleteDate;
                existingDelivery.DeliveryComplete = progress.DeliveryComplete;
                existingDelivery.DeliveryCompleteDescription = progress.DeliveryCompleteDescription;
                existingDelivery.WarrentyYears = progress.WarrentyYears;
                existingDelivery.UpdatedDate = DateTime.Now;

                // Update other properties as needed
                await _context.SaveChangesAsync();
            }

            var ManufacturingItems = await _context.ManufacturingProgresses.FindAsync(progress.ManufacturingStatusId);

            if (ManufacturingItems != null)
            {

                // Update existing Delivery
                ManufacturingItems.DeliveryComplete = progress.DeliveryComplete;
                existingDelivery.UpdatedDate = DateTime.Now;

                // Update other properties as needed
                await _context.SaveChangesAsync();
            }


            //var InventoryItems = await _context.Inventory.Where(Inventorys => Inventorys.AircraftId == progress.AircraftId).ToListAsync();
            var InventoryItems = await _context.Inventory
             .FirstOrDefaultAsync(Inventorys => Inventorys.AircraftId == progress.AircraftId);


            var stockReducedSeatingCapacity = existingDelivery.SeatingCapacity;
            var stockReducedEngines = 2;
            var stockReducedAircraftBody = 1;
            var stockReducedAirframes = 2;
            var stockReducedAvionicsSystems = 2;
            var stockReducedFuelTanks = 2;


            if (InventoryItems != null)
            {

                // Update existing Delivery
                InventoryItems.InventorySeats = InventoryItems.InventorySeats - stockReducedSeatingCapacity;
                InventoryItems.InventoryEngines = InventoryItems.InventoryEngines - stockReducedEngines;
                InventoryItems.InventoryAircraftBody = InventoryItems.InventoryAircraftBody - stockReducedAircraftBody;
                InventoryItems.InventoryAirframes = InventoryItems.InventoryAirframes - stockReducedAirframes;
                InventoryItems.InventoryAvionicsSystems = InventoryItems.InventoryAvionicsSystems - stockReducedAvionicsSystems;
                InventoryItems.InventoryFuelTanks = InventoryItems.InventoryFuelTanks - stockReducedFuelTanks;

                InventoryItems.UpdatedDate = DateTime.Now;

                // Update other properties as needed
                await _context.SaveChangesAsync();
            }

            var AircraftItems = await _context.Aircrafts.FindAsync(progress.AircraftId);

            if (AircraftItems != null && InventoryItems != null)
            {

                // Update existing Delivery
                AircraftItems.InventoryEngines = AircraftItems.InventoryEngines - stockReducedEngines;
                AircraftItems.UpdatedDate = DateTime.Now;

                // Update other properties as needed
                await _context.SaveChangesAsync();
            }




            await SendContactEmail(progress);
            await SendDeliveryAsync(progress);

        }

        private async Task SendContactEmail(Delivery Delivery)
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
                    mailMessage.Subject = "Delivery Success";
                    mailMessage.Body = $"Owner Name: {Delivery.CardHolderName}, \nModel Name: {Delivery.ModelName}, \nManufacturer: {Delivery.Manufacturer},\nContact Number: {Delivery.Telephone},\nPrice: {Delivery.BasePrice} USD, \nAirframe Progress: {Delivery.AirframeProgress}, \nEngines Progress: {Delivery.EnginesProgress}, \nInterior Progress: {Delivery.InteriorProgress}, \nAvionics Systems Progress: {Delivery.AvionicsSystemsProgress}, \nOverall Progress: {Delivery.OverallProgress}, \nDelivery Date: {Delivery.DeliveryDate}, \nEmail Date: {DateTime.Now}, \nAirport City : {Delivery.AirportCity}, \nShipping Date : {Delivery.ShippingDate}";
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

        public async Task<bool> SendDeliveryAsync(Delivery Delivery)
        {

            string userId = "2609511";
            string apiKey = "gQ3MtBPvVecLY9s7OeDb";
            string senderId = "NotifyDEMO";
            string recipientNumber = "94777386791"; // Replace with recipient's phone number
            string message = "Raythos Aerospace System Aircraft Delivery Success  " + "\nCustomer Name : " + Delivery.CardHolderName + "\nDelivery Date : " +  Delivery.DeliveryDate + "\nAirport City : " + Delivery.AirportCity; // Replace with your OTP message

            Console.WriteLine(message);

            using (var httpClient = new HttpClient())
            {
                var sendSmsUrl = $"https://app.notify.lk/api/v1/send?user_id={userId}&api_key={apiKey}&sender_id={senderId}&to={recipientNumber}&message={message}";

                var response = await httpClient.GetAsync(sendSmsUrl);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("SMS sent successfully!");
                }
                else
                {
                    Console.WriteLine("Failed to send SMS. Status code: " + response.StatusCode);
                }

                return response.IsSuccessStatusCode;
            }

        }

        public async Task DeleteAsync(Delivery Delivery)
        {
            _context.Delivery.Remove(Delivery);
            await _context.SaveChangesAsync();
        }
    }
}
