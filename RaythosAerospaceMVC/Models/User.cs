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
    }
}
