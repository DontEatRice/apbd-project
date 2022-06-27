namespace Server.Models.PolygonModels
{
    public class Publisher
    {
        public string? name { get; set; }
        public string? homepage_url { get; set; }
        public string? logo_url { get; set; } 
        public string? favicon_url { get; set; } 
    }

    public class News
    {
        public string id { get; set; } = null!;
        public Publisher? publisher { get; set; }
        public string title { get; set; } = null!;
        public string author { get; set; } = null!;
        public DateTime published_utc { get; set; }
        public string article_url { get; set; } = null!;
        public List<string>? tickers { get; set; }
        public string? amp_url { get; set; }
        public string? image_url { get; set; }
        public string description { get; set; } = null!;
        public List<string>? keywords { get; set; }
    }

    public class TickerNews
    {
        public List<News> results { get; set; } = null!;
        public string? status { get; set; }
        public string? request_id { get; set; }
        public int count { get; set; }
        public string? next_url { get; set; }
    }
}