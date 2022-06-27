namespace blazor_project.Shared.Models.DTOs
{
    public class TickerNews
    {
        public string title { get; set; } = null!;
        public string author { get; set; } = null!;
        public string article_url { get; set; } = null!;
        public string description { get; set; } = null!;
        public DateTime published_utc { get; set; }
    }
}