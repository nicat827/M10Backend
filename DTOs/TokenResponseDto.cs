namespace M10Backend.DTOs
{
    public record TokenResponseDto(string AccessToken, string RefreshToken, DateTime RefreshTokenExpiresAt);
}
