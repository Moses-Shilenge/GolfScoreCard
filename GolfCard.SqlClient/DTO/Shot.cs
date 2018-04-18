using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolfCard.SqlClient.DTO
{
    public class Shot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Tee Tee { get; set; }
        public int Shots { get; set; }
    }
}