using System.ComponentModel.DataAnnotations.Schema;

namespace RaythosAerospaceMVC.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? PasswordSalt { get; internal set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }

        public string? ImageUrl { get; set; }

        [NotMapped]
        public IFormFile? uploadedImage { get; set; }



    }
}
