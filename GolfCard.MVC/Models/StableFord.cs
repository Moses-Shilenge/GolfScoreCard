using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GolfCard.MVC.Models
{
    public class StableFord
    {
        public Guid Id { get; set; }
        public string Player { get; set; }
        [RequiredIfTrue("Player")]
        public List<int> Hole { get; set; }
        public List<int> Points { get; set; }
        public int IN { get; set; }
        public int OUT { get; set; }
        public int TOT { get; set; }
        public int HandiCap { get; set; }
        public int NET { get; set; }
        public int StableFordsPoints { get; set; }
    }
}