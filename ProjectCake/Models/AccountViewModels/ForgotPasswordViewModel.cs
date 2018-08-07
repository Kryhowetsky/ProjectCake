using System.ComponentModel.DataAnnotations;

namespace ProjectCake.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}