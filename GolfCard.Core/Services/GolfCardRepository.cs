using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GolfCard.SqlClient.DTO;
using GolfCard.SqlClient;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace GolfCard.Core.Services
{
    public class GolfCardRepository : IGolfCardRepository
    {
        private SQLDBContext _sqlConnect;

        public GolfCardRepository()
        {
            _sqlConnect = new SQLDBContext();
        }

        public void DeleteCardById(Guid Id)
        {
            var game = GetGameById(Id);

            _sqlConnect.Database.ExecuteSqlCommand($"delete from Shots where game_Id = '{game.Id}' delete from Games where Id = '{game.Id}'");
            _sqlConnect.SaveChanges();
        }

        public void EditCard(Game game)
        {
            _sqlConnect.Set<Game>().AddOrUpdate(game);
            _sqlConnect.Set<Player>().AddOrUpdate(game.Player);

            var sqlCommands = new List<string>();

            foreach (var item in game.Shots)
            {
                // not the best approach, but for now till I get a better way to update the shot. I will use this
                sqlCommands.Add($"update shots set shots = {item.Shots} where tee_Id = '{item.Tee.Id}' and game_Id = '{game.Id}'");                
            }

            var updateStatements = string.Join("\n", sqlCommands);

            _sqlConnect.Database.ExecuteSqlCommand(updateStatements);
            _sqlConnect.SaveChanges();
        }

        public IList<Tee> GetAllParsFromTees()
        {
            return _sqlConnect.Tees.ToList();
        }

        public Game GetGameById(Guid Id)
        {
            return GetScoreBoard().Where(g => g.Id == Id).FirstOrDefault();
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
                    IN = game.IN,
                    OUT = game.OUT,
                    NET = game.NET,
                    TOT = game.OUT,
                    Id = game.Id
                });
            }

            return gameColl;
        }

        public void CreateCard(IEnumerable<Game> playerScores)
        {
            foreach (var item in playerScores)
            {
                var player = _sqlConnect.Players.Where(p => p.Name == item.Player.Name).FirstOrDefault();

                if (player != null) { _sqlConnect.Players.Attach(item.Player); }

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
        public int IN { get; set; }
        public int OUT { get; set; }
        public int TOT { get; set; }
        public int HCP { get; set; }
        public int NET { get; set; }
        public DateTime Date { get; set; }
    }
}
