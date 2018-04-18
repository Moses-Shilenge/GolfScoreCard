using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GolfCard.SqlClient.DTO
{
    public class Game
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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