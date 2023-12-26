﻿using System;
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


            await SendContactEmail(progress);
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

        public async Task DeleteAsync(Delivery Delivery)
        {
            _context.Delivery.Remove(Delivery);
            await _context.SaveChangesAsync();
        }
    }
}