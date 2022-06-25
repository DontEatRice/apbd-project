using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blazor_project.Shared.Models;

namespace blazor_project.Shared.Models.DTOs
{
    public class TickerSearch : Ticker
    {
        public string? Name { get; set; }
    }
}