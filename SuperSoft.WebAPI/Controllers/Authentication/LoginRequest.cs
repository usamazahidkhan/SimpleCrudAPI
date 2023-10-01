using System.ComponentModel.DataAnnotations;

namespace SuperSoft.WebAPI.Controllers.Authentication
{
    public sealed class LoginRequest
    {
        [Required]
        public string userName { get; set; }
        
        [Required]
        public string password { get; set; }
    }
}