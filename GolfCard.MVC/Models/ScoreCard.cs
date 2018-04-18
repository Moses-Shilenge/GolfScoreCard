using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GolfCard.MVC.Models
{
    public class ScoreCardView
    {
        public Guid Id { get; set; }
        public string Player { get; set; }
        [RequiredIfTrue("Player")]
        public List<int> Hole { get; set; }
        public int IN_1 { get; set; }
        public int IN_2 { get; set; }
        public int OUT { get; set; }
        public int HandiCap { get; set; }
        public int NET { get; set; }
    }
}