using System.ComponentModel.DataAnnotations;

namespace SuperSoft.WebAPI.Controllers.Students
{
    public sealed class StudentRequest
    {
        [Required]
        [MaxLength(50)]
        public string name { get; set; }
        public DateTime dateOfBirth { get; set; }
    }
}