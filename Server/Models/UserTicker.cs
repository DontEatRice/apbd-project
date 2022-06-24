namespace Server.Models
{
    public class UserTicker
    {
        public int IdTicker {get; set;}
        public string IdUser { get; set; } = null!;
        public virtual Ticker Ticker { get; set; } = null!;
        public virtual ApplicationUser User { get; set; } = null!;
    }
}