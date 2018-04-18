using GolfCard.SqlClient.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfCard.SqlClient
{
    public class SQLDBContext : DbContext
    {
        public SQLDBContext() : base("DefaultConnection")
        {
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Tee> Tees { get; set; }
        public DbSet<Shot> Shots { get; set; }
    }
}
