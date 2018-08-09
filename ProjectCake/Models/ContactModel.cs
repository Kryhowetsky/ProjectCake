using Microsoft.AspNetCore.Mvc;

namespace ProjectCake.Models
{
    public class ContactModel
    {
        [BindProperty]
        public ContactFormModel Contact { get; set; }
    }
}