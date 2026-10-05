using System.ComponentModel.DataAnnotations;

namespace GMServerWebPanel.API.Models
{
    public class User
    {
        [Key]
        public required string Login { get; init; }

        [Required]
        public required string Password { get; init; }
    }
}
