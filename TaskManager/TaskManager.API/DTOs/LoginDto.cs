using System.ComponentModel.DataAnnotations;

namespace TaskManager.API.DTOs
{
    public class LoginDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
