using Microsoft.AspNetCore.Identity;

namespace M10Backend.Entities
{
    public class AppUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiresAt { get; set; }

        public ICollection<OTPCode> OTPCodes { get; set; } = new List<OTPCode>();
    }
}
