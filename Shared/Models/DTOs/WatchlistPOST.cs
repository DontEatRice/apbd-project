using System.ComponentModel.DataAnnotations;

namespace blazor_project.Shared.Models.DTOs
{
    public class WatchlistPOST
    {
        [Required]
        [MaxLength(16)]
        public string TickerSymbol {get; set;} = null!;
    }
}