namespace RaythosAerospaceMVC.Models
{
    public class Otps
    {
        public int Id { get; set; } // Assuming OTP has an ID
        public string? Otp { get; set; }
        public int? UserId { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? Telephone { get; set;}

    }
}
