using System;

namespace GolfCard.MVC.Models
{
    public class Shot
    {
        public Guid Id { get; set; }
        public Hole Tee { get; set; }
        public int Shots { get; set; }
    }
}