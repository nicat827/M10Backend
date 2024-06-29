namespace M10Backend.DTOs
{
    public class ConfirmOTPDto
    {
        public int OTP { get; set; }
        public string Phone { get; set; } = null!;
    }
}
