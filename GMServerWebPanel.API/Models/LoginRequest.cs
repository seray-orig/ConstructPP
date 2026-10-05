using System.ComponentModel.DataAnnotations;

namespace GMServerWebPanel.API.Models
{
    public readonly struct LoginRequest
    {
        [Required]
        public required string Login
        {
            get;
            init
            {
                if (!string.IsNullOrEmpty(value) && value.Length <= 36)
                    field = value;
            }
        }

        [Required]
        public required string Password
        {
            get;
            init
            {
                if (!string.IsNullOrEmpty(value) && value.Length <= 64)
                    field = value;
            }
        }

        public bool RememberMe { get; init; }
    }
}
