using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using RaythosAerospaceMVC.Models;
using System.Net.Mail;
using System.Net;

namespace RaythosAerospaceMVC.Repositories
{
    public class ManufacturingProgressRepository : IManufacturingProgressRepository
    {
        private readonly AerospaceDbContext _context;

        public ManufacturingProgressRepository(AerospaceDbContext context)
        {
            _context = context;
        }

        public async Task<List<ManufacturingProgress>> GetAllAsync()
        {
            return await _context.ManufacturingProgresses.ToListAsync();
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

            existingAircraft.Description = progress.Description;
            existingAircraft.AirframeProgress = progress.AirframeProgress;
            existingAircraft.AvionicsSystemsProgress = progress.AvionicsSystemsProgress;
            existingAircraft.EnginesProgress = progress.EnginesProgress;
            existingAircraft.InteriorProgress = progress.InteriorProgress;
            existingAircraft.OverallProgress = progress.OverallProgress;
            existingAircraft.DeliveryDate = progress.DeliveryDate;
            existingAircraft.ManufacturingComplete = progress.ManufacturingComplete;
            existingAircraft.UpdatedDate = progress.UpdatedDate;



            // Copy other properties as needed
            //};

            // Update the properties of the existingAircraft entity with the new values
            //_context.Entry(existingAircraft).CurrentValues.SetValues(newAircraft);

            await _context.SaveChangesAsync();

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
                    mailMessage.Subject = "Manufacturing Progress Update";
                    mailMessage.Body = $"Owner Name: {ManufacturingProgress.CardHolderName}, \nModel Name: {ManufacturingProgress.ModelName}, \nManufacturer: {ManufacturingProgress.Manufacturer},\nContact Number: {ManufacturingProgress.Telephone},\nPrice: {ManufacturingProgress.BasePrice} USD, \nAirframe Progress: {ManufacturingProgress.AirframeProgress}, \nEngines Progress: {ManufacturingProgress.EnginesProgress}, \nInterior Progress: {ManufacturingProgress.InteriorProgress}, \nAvionics Systems Progress: {ManufacturingProgress.AvionicsSystemsProgress}, \nOverall Progress: {ManufacturingProgress.OverallProgress}, \nDelivery Date: {ManufacturingProgress.DeliveryDate}, \nEmail Date: {DateTime.Now}";
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
