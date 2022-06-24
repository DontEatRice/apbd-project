namespace Server.Models
{
    public class Ticker : blazor_project.Shared.Models.Ticker
    {
        public virtual ICollection<UserTicker> UsersTickers {get; set;} = null!;
    }
}