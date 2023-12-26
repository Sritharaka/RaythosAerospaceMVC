using System.ComponentModel.DataAnnotations;

namespace RaythosAerospaceMVC.Models
{
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }

        [Required(ErrorMessage = "Please enter the first name")]
        [StringLength(50, ErrorMessage = "First name must not exceed 50 characters")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Please enter the last name")]
        [StringLength(50, ErrorMessage = "Last name must not exceed 50 characters")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Please enter the email address")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(100, ErrorMessage = "Email address must not exceed 100 characters")]
        public string? Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [StringLength(20, ErrorMessage = "Phone number must not exceed 20 characters")]
        public string? PhoneNumber { get; set; }

        public string? Message { get; set; }

        public int?  UserId { get; set; }

        // Additional fields for address, organization, etc. can be added here

        // Date the contact record was created
        public DateTime? CreatedDate { get; set; }

        // Date the contact record was last updated
        public DateTime? UpdatedDate { get; set; }
    }
}
