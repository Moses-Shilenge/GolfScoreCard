using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolfCard.SqlClient.DTO
{
    public class Tee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public int Index { get; set; }
        public int Yards { get; set; }
        public int Par { get; set; }
    }
}