using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GolfCard.SqlClient.DTO;
using GolfCard.SqlClient;

namespace GolfCard.Core.Services
{
    public class GolfCardRepository : IGolfCardRepository
    {
        private SQLDBContext _sqlConnect;

        public GolfCardRepository()
        {
            _sqlConnect = new SQLDBContext();
        }

        public IEnumerable<Game> GetScoreBoard()
        {
            var gameColl = new List<Game>();

            var games = _sqlConnect.Database.SqlQuery<SQLGameView>($"select * from Games order by Date desc").Take(5).Distinct();

            foreach (var game in games)
            {
                var shots = _sqlConnect.Database.SqlQuery<Shot>($"select * from Shots where Game_Id = '{game.Id}'").ToList();
                var player = _sqlConnect.Database.SqlQuery<Player>($"select * from Players where Id = '{game.Player_Id}'").FirstOrDefault();

                gameColl.Add(new Game
                {
                    Player = player,
                    Shots = shots,
                    HCP = game.HCP,
                    Date = game.Date,
                    IN_1 = game.IN_1,
                    IN_2 = game.IN_2,
                    NET = game.NET,
                    OUT = game.OUT,
                    Id = game.Id
                });
            }

            return gameColl;
        }

        public void SaveCard(IEnumerable<Game> playerScores)
        {
            foreach (var item in playerScores)
            {
                foreach (var tee in item.Shots)
                {
                    _sqlConnect.Tees.Attach(tee.Tee);
                }

                _sqlConnect.Games.Add(item);
            }

            _sqlConnect.SaveChanges();
        }
    }

    public class SQLGameView
    {
        public Guid Id { get; set; }
        public Guid Player_Id { get; set; }
        public int IN_1 { get; set; }
        public int IN_2 { get; set; }
        public int OUT { get; set; }
        public int HCP { get; set; }
        public int NET { get; set; }
        public DateTime Date { get; set; }
    }
}
