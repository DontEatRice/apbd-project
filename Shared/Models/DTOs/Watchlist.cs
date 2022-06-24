namespace blazor_project.Shared.Models.DTOs
{
    public class Watchlist
    {
        public IEnumerable<Ticker> Tickers {get; set;} = null!;       
    }
}