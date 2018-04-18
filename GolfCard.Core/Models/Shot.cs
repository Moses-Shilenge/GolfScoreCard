using System;

namespace GolfCard.Core
{
    public class Shot
    {
        public Guid Id { get; set; }
        public Hole Tee { get; set; }
        public int Shots { get; set; }
    }
}