namespace Server.Models.PolygonModels
{
    public class Price
    {
        public double c { get; set; }
        public double h { get; set; }
        public double l { get; set; }
        public int n { get; set; }
        public double o { get; set; }
        public long t { get; set; }
        public double v { get; set; }
        public double vw { get; set; }
    }

    public class TickerPrice
    {
        public bool adjusted { get; set; }
        public int queryCount { get; set; }
        public string request_id { get; set; } = null!;
        public List<Price>? results { get; set; } = null!;
        public int resultsCount { get; set; }
        public string status { get; set; } = null!;
        public string ticker { get; set; } = null!;
    }
}