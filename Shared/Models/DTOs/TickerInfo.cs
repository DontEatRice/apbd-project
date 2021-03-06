namespace blazor_project.Shared.Models.DTOs
{
    public class Branding
    {
        public string icon_url { get; set; } = null!;
        public string logo_url { get; set; } = null!;
    }
    public class TickerInfo
    {
        public string locale { get; set; } = null!;
        public string market { get; set; } = null!;
        public string description { get; set; } = null!;
        public string primary_exchange { get; set; } = null!;
        public Branding? branding { get; set; }
        public int total_employees { get; set; }
        public string name { get; set; } = null!;
    }
}