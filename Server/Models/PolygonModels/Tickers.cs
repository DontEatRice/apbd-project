namespace Server.Models.PolygonModels
{
    public class Result
    {
        public string ticker { get; set; } = null!;
        public string name { get; set; } = null!;
    }

    public class Tickers
    {
        public List<Result> results { get; set; } = null!;
        public string status { get; set; } = null!;
        public int count { get; set; }
        public string? next_url { get; set; }
    }
}