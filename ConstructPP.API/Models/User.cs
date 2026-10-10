using System.ComponentModel.DataAnnotations;

namespace ConstructPP.API.Models
{
    public class User
    {
        [Key]
        public required string Login { get; init; }

        [Required]
        public required string Password { get; init; }
    }
}
