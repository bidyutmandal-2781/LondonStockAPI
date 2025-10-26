namespace LondonStockAPI.Exceptions
{
    public class ErrorResponse
    {
        public string Title { get; set; } = string.Empty;
        public int Status { get; set; }
        public string? Detail { get; set; }
        public string TraceId { get; set; } = string.Empty;
        public string Instance { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public Dictionary<string, string[]>? Errors { get; set; }
    }
}
