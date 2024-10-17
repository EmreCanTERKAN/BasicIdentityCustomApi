using System.ComponentModel.DataAnnotations;

namespace BasicIdentityCustomApi.Models
{
    public class RegisterRequest
    {
        [EmailAddress]
        [Required]
        [MinLength(10)]
        public string Email { get; set; }

        [Required]
        [Length(5,20)]
        public string Password { get; set; }
        // Model Validation
    }
}
