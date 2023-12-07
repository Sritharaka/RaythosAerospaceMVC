using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using RaythosAerospaceMVC.Models;
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

    }
}
