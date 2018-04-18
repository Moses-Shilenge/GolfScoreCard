using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GolfCard.Core
{
    public class Game
    {
        public Guid Id { get; set; }
        public Player Player { get; set; }
        public List<Shot> Shots { get; set; }
        public int IN_1 { get; set; }
        public int IN_2 { get; set; }
        public int OUT { get; set; }
        public int HCP { get; set; }
        public int NET { get; set; }
        public DateTime Date { get; set; }
    }
}