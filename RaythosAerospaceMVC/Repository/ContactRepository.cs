using RaythosAerospaceMVC.Models;
using System.Net.Mail;
using System.Net;

namespace RaythosAerospaceMVC.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly AerospaceDbContext _context; // Replace with your DbContext
        private readonly SmtpClient _smtpClient; // You can configure SMTP settings

        public ContactRepository(AerospaceDbContext context, SmtpClient smtpClient)
        {
            _context = context;
            _smtpClient = smtpClient;
            //// Configure your SMTP client here
            //_smtpClient = new SmtpClient("smtp.example.com")
            //{
            //    Port = 587,
            //    Credentials = new NetworkCredential("keminda4lk@gmail.com", "jvdsgagljwyomquz"),
            //    EnableSsl = true,
            //};
        }

        public async Task<Contact> CreateContact(Contact contact)
        {
            // Save contact to the database
            _context.Contact.Add(contact);
            await _context.SaveChangesAsync();

            // Send an email
            await SendContactEmail(contact);

            return contact;
        }
        private async Task SendContactEmail(Contact contact)
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
                    mailMessage.Subject = "New Contact Request";
                    mailMessage.Body = $"Name: {contact.FirstName} {contact.LastName} \nEmail: {contact.Email}\nContact Number: {contact.PhoneNumber}\nMessage: {contact.Message}";
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

    }
}
