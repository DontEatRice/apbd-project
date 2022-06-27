namespace blazor_project.Shared.Models.DTOs
{
    public class TickerPrice
    {
        public double close { get; set; }
        public double high { get; set; }
        public double low { get; set; }
        public double open { get; set; }
        public DateTime time {get; set; } 
        public double volume { get; set; }
        // public string axisTime {
        //     get {
        //         if (time.get)
        //     }
        // }
    }
}