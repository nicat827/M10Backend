using M10Backend.DTOs;
using M10Backend.Entities;
using M10Backend.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace M10Backend.Services
{
    public class TokenService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public TokenService(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<(string, DateTime)> GenerateAccessTokenAsync(AppUser user)
        {
            ICollection<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };
            foreach (UserRoleEnum role in Enum.GetValues(typeof(UserRoleEnum)))
            {
                if (await _userManager.IsInRoleAsync(user, role.ToString()))
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
                }
            }
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecurityKey"]));
            SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: signingCredentials

            );
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            string accessToken = tokenHandler.WriteToken(token);
            return (accessToken, token.ValidTo);
        }
        public async Task<TokenResponseDto> GenerateTokensAsync(AppUser user)
        {
            string refreshToken = Guid.NewGuid().ToString();
            (string accessToken, DateTime validTo) = await GenerateAccessTokenAsync(user);
            return new TokenResponseDto(
                accessToken,
                refreshToken,
                validTo.AddMinutes(30)
            );
        }
    }
}
