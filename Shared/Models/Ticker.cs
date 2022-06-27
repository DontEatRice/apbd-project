namespace blazor_project.Shared.Models
{
    public class Ticker
    {
        public int IdTicker { get; set; }
        public string TickerSymbol { get; set; } = null!;
        public string? LogoUrl {get; set;}
        public string? Name {get; set;}
        public string? Sic {get; set;}
    }
}