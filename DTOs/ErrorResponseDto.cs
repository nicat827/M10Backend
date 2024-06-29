namespace M10Backend.DTOs
{
    public class ErrorResponseDto
    {
        public int Status { get; set; }
        public string Message { get; set; } = null!;
        public Dictionary<string, IEnumerable<string>>? Errors { get; set; }
    }
}
