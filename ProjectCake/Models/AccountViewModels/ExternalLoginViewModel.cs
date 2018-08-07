using System.ComponentModel.DataAnnotations;

namespace ProjectCake.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}