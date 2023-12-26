using Microsoft.EntityFrameworkCore;
using RaythosAerospaceMVC.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RaythosAerospaceMVC.Repository
{
    public class OtpRepository : IOtpRepository
    {
        private readonly AerospaceDbContext _context;

        public OtpRepository(AerospaceDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SendOtpAsync(string phoneNumber, string otp)
        {

            string userId = "2609511";
            string apiKey = "gQ3MtBPvVecLY9s7OeDb";
            string senderId = "NotifyDEMO";
            string recipientNumber = phoneNumber; // Replace with recipient's phone number
            string message = "Raythos Aerospace System Aircraft Payment OTP : " + otp; // Replace with your OTP message

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

        public async Task<Otps> SaveOtpAsync(Otps otp)
        {
            try
            {
                if (otp != null)
                {
                    _context.Otps.Add(otp);
                    await _context.SaveChangesAsync();
                }

                return otp;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("Database error: " + ex.Message);
                throw; // Rethrow the exception to handle it at a higher level if needed
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                throw; // Rethrow the exception to handle it at a higher level if needed
            }
        }
    }
}

