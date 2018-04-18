using System;

namespace GolfCard.Core
{
    public class Hole
    {
        public Guid Id { get; set; }
        public int Index { get; set; }
        public int Yards { get; set; }
        public int Par { get; set; }
    }
}