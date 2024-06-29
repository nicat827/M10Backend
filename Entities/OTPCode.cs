namespace M10Backend.Entities
{
    public class OTPCode
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public DateTime ValidTo { get; set; }
        public string UserId { get; set; } = null!;
        public AppUser User { get; set; } = null!;
    }
}
